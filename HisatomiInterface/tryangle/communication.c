/****************************************************************************/
/* ファイル名 : communication.c												*/
/* 概要       : 通信クラス													*/
/****************************************************************************/

/****************************************************************************/
/* インクルードファイル														*/
/****************************************************************************/
#include "kernel.h"
#include "kernel_id.h"
#include "system.h"
#include "communication.h"
#include <string.h>

/****************************************************************************/
/* define定義・マクロ定義													*/
/****************************************************************************/
#define COMM_MAX_SIZE		(16)


/****************************************************************************/
/* クラス属性																*/
/****************************************************************************/
typedef enum {								/* 受信データ解析状態			*/
	STATE_WAIT,								/*   開始受信待ち				*/
	STATE_CMD,								/*   コマンド受信中				*/
} eRxState;

typedef struct {
	S8		btRxBuff[BT_MAX_RX_BUF_SIZE];	/* Bluetooth用受信バッファ		*/
	S8		rxFrameData[COMM_MAX_SIZE];		/* 受信データバッファ			*/
	S8		rxFrameSize;					/* 受信データサイズ				*/
	S8		cmd;							/* 受信コマンド種別				*/
	S8		value;							/* 受信コマンドデータ値			*/
	eRxState	rxStatus;					/* 受信データ解析状態			*/
} COMM;



/****************************************************************************/
/* クラス操作(Private)														*/
/****************************************************************************/
static int analyzeRxData(void);				/* 受信データを解析する			*/


/****************************************************************************/
/* インスタンス																*/
/****************************************************************************/
static COMM		sComm;						/* 通信							*/


/****************************************************************************/
/* 関数名	: COMM_initializeDevice											*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: デバイスを初期化する											*/
/****************************************************************************/
void COMM_initializeDevice(void)
{
	/* Bluetoothデバイスの初期化 */
	ecrobot_init_bt_slave(PASS_KEY);
}

/****************************************************************************/
/* 関数名	: COMM_terminate												*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: デバイスを終了する											*/
/****************************************************************************/
void COMM_terminateDevice(void)
{
	/* Bluetooth通信終了 */
	ecrobot_term_bt_connection();
}

/****************************************************************************/
/* 関数名	: COMM_initialize												*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: 初期化する													*/
/****************************************************************************/
void COMM_initialize(void)
{
	/* Bluetoothデバイス名の設定 */
	ecrobot_set_bt_device_name(DEVICE_NAME);

	sComm.cmd = COMM_CMD_STOP;
	sComm.value = 0;
	sComm.rxStatus = STATE_WAIT;
}

/****************************************************************************/
/* 関数名	: COMM_isReceived												*/
/* 引数		: なし															*/
/* 戻り値	: 受信結果														*/
/*          :   0 : 受信データ無し											*/
/*          :   1 : 受信データ有り											*/
/* 概要		: 受信データ有無を取得する										*/
/****************************************************************************/
int COMM_isReceived(void)
{
	int	i;
	int	ret = 0;
	U32	rxlength = 0;

	if(ecrobot_get_bt_status() == BT_STREAM){
		rxlength = ecrobot_read_bt(sComm.btRxBuff, 0, BT_MAX_RX_BUF_SIZE);
	}
	
	if(rxlength > 0){
		for(i = 0; i < rxlength; i++){
			switch(sComm.rxStatus){
				case STATE_WAIT:
					if(sComm.btRxBuff[i] == '<'){
						sComm.rxFrameSize = 0;
						sComm.rxFrameData[sComm.rxFrameSize] = sComm.btRxBuff[i];
						sComm.rxFrameSize++;
						sComm.rxStatus = STATE_CMD;
					}
					break;
				case STATE_CMD:
					sComm.rxFrameData[sComm.rxFrameSize] = sComm.btRxBuff[i];
					sComm.rxFrameSize++;
					if(sComm.btRxBuff[i] == '>'){
						/* コマンド受信完了 */
						sComm.rxFrameData[sComm.rxFrameSize] = '\0';
#if 1
						/* デバッグ用に受信データをLCDに表示 */
						display_clear(0);
						display_goto_xy(0, 1);
						display_string((const char*)sComm.rxFrameData);
						display_update();
#endif
						/* 受信データを解析 */
						if(analyzeRxData() == 1){
							/* 有効なデータの場合はレスポンスを送信 */
							ecrobot_send_bt("<OK>", 0, 4);
							ret = 1;
						}
						sComm.rxFrameSize = 0;
						sComm.rxStatus = STATE_WAIT;
					}
					break;
				default:
					sComm.rxFrameSize = 0;
					sComm.rxStatus = STATE_WAIT;
			}
			if(sComm.rxFrameSize > COMM_MAX_SIZE){
				sComm.rxFrameSize = 0;
				sComm.rxStatus = STATE_WAIT;
			}
		}
	}

	return(ret);
}

/****************************************************************************/
/* 関数名	: COMM_getCmdInfo												*/
/* 引数		: cmd   : コマンド												*/
/*			: value : 値													*/
/* 戻り値	: なし															*/
/* 概要		: コマンド情報を取得する										*/
/****************************************************************************/
void COMM_getCmdInfo(S8 *cmd, S8 *value)
{
	*cmd = sComm.cmd;
	*value = sComm.value;
}

/****************************************************************************/
/* 関数名	: analyzeRxData													*/
/* 引数		: なし															*/
/* 戻り値	: 解析結果														*/
/*          :   0 : 有効データ無し											*/
/*          :   1 : 有効データ有り											*/
/* 概要		: 受信データを解析する											*/
/*			:																*/
/*			:	 <START>	走行開始										*/
/*			:	 <STOP>		走行停止										*/
/*			:	 <Fxxx>		前進(xxx:0～100)								*/
/*			:	 <Bxxx>		後進(xxx:0～100)								*/
/*			:	 <Rxxx>		右旋回(xxx:0～100)								*/
/*			:	 <Lxxx>		左旋回(xxx:0～100)								*/
/****************************************************************************/
static int analyzeRxData(void)
{
	int	ret = 0;							/* 解析結果						*/
	int	value;								/* データ値						*/

	/* 走行開始チェック */
	if(strcmp("<START>", (const char*)sComm.rxFrameData) == 0){
		sComm.cmd = COMM_CMD_START;
		ret = 1;
	}
	/* 走行停止チェック */
	else if(strcmp("<STOP>", (const char*)sComm.rxFrameData) == 0){
		sComm.cmd = COMM_CMD_STOP;
		ret = 1;
	}
	/* 前後進・旋回チェック */
	else {
		/* 数字部分の取得 */
		value = 0;
		if((sComm.rxFrameSize - 3) == 1){			/* 数字部分が1桁 */
			value = sComm.rxFrameData[2] - '0';
		}
		else if((sComm.rxFrameSize - 3) == 2){		/* 数字部分が2桁 */
			value = ((sComm.rxFrameData[2] - '0') * 10)
				+  (sComm.rxFrameData[3] - '0');
		}
		else if((sComm.rxFrameSize - 3) == 3){		/* 数字部分が3桁 */
			value= ((sComm.rxFrameData[2] - '0') * 100)
				+ ((sComm.rxFrameData[3] - '0') * 10)
				+  (sComm.rxFrameData[4] - '0');
		}
		/* 最小値/最大値チェック */
		if(value < 0){
			value = 0;
		}
		else if(value > 100){
			value = 100;
		}

		if(value==0){
			sComm.cmd = COMM_CMD_ZERO;
			sComm.value = value;
			ret = 1;
		}
		else if(sComm.rxFrameData[1] == 'F'){
			sComm.cmd = COMM_CMD_FB;
			sComm.value = value;
			ret = 1;
		}
		else if(sComm.rxFrameData[1] == 'B'){
			sComm.cmd = COMM_CMD_FB;
			sComm.value = value * (-1);
			ret = 1;
		}
		else if(sComm.rxFrameData[1] == 'R'){
			sComm.cmd = COMM_CMD_LR;
			sComm.value = value;
			ret = 1;
		}
		else if(sComm.rxFrameData[1] == 'L'){
			sComm.cmd = COMM_CMD_LR;
			sComm.value = value * (-1);
			ret = 1;
		}
	}

	return(ret);
}
