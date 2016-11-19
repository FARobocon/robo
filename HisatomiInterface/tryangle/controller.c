/****************************************************************************/
/* ファイル名 : controller.c												*/
/* 概要       : 走行コントローラクラス										*/
/****************************************************************************/

/****************************************************************************/
/* インクルードファイル														*/
/****************************************************************************/
#include "kernel.h"
#include "kernel_id.h"
#include "system.h"
#include "controller.h"
#include "touch.h"
#include "tail.h"
#include "communication.h"
#include "balancer.h"						/* 倒立振子制御用ヘッダファイル */

/****************************************************************************/
/* define定義・マクロ定義													*/
/****************************************************************************/

/****************************************************************************/
/* クラス属性																*/
/****************************************************************************/
typedef enum {								/* 走行状態						*/
	STATE_CALIB,							/*   キャリブレーション中		*/
	STATE_READY,							/*   準備中						*/
	STATE_WAIT,								/*   走行待ち					*/
	STATE_RUNSTART,							/*   走行開始					*/
	STATE_RUN,								/*   走行中						*/
	STATE_STOPREADY,						/*   停止準備					*/
	STATE_STOP								/*   停止中						*/
} eRunState;

typedef struct {
	eRunState	runStatus;					/* 走行状態						*/
	U16		count100ms;						/* 100ms計測カウンタ			*/
	U16		waitTimer;						/* 待ち用タイマ（4ms毎に減算）	*/
	U16		gyroOffset;						/* ジャイロオフセット			*/
	S8		forward;						/* 前後進命令					*/
											/* (-100～+100 +:前/-:後)		*/
	S8		turn;							/* 旋回命令						*/
											/* (-100～+100 +:右/-:左)		*/
} CTRL;

/****************************************************************************/
/* クラス操作(Private)														*/
/****************************************************************************/
static void run(void);						/* 走行する						*/
static void stop(void);						/* 停止する						*/
static void beep(void);						/* ビープ音を鳴らす				*/

/****************************************************************************/
/* インスタンス																*/
/****************************************************************************/
static CTRL		sCtrl;						/* 走行制御						*/


/****************************************************************************/
/* 関数名	: CTRL_initializeDevice											*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: デバイスを初期化する											*/
/****************************************************************************/
void CTRL_initializeDevice(void)
{
	/* 停止 */
	stop();

	/* 通信デバイスの初期化 */
	COMM_initializeDevice();
}

/****************************************************************************/
/* 関数名	: CTRL_terminateDevice											*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: デバイスを終了する											*/
/****************************************************************************/
void CTRL_terminateDevice(void)
{
	/* 通信デバイスの終了 */
	COMM_terminateDevice();
}

/****************************************************************************/
/* 関数名	: CTRL_initialize												*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: 初期化する													*/
/****************************************************************************/
void CTRL_initialize(void)
{
	TOUCH_initialize();						/* タッチセンサの初期化			*/
	COMM_initialize();						/* 通信の初期化					*/
	TAIL_initialize();						/* 尻尾の初期化					*/

	sCtrl.count100ms = 0;
	sCtrl.waitTimer  = 0;
	sCtrl.forward = 0;
	sCtrl.turn    = 0;
	sCtrl.runStatus = STATE_CALIB;
}

/****************************************************************************/
/* 関数名	: CTRL_execute													*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: 制御を実行する												*/
/* 特記事項	: 4ms間隔で実行すること											*/
/****************************************************************************/
void CTRL_execute(void)
{
	S8	startflg = false;
	S8	stopflg  = false;
	S8	cmd;
	S8	value;

	/* 通信受信チェック */
	if(COMM_isReceived() == 1){
		COMM_getCmdInfo(&cmd, &value);
		if(cmd == COMM_CMD_START){				/* 走行開始					*/
			startflg = true;
		}
		else if(cmd == COMM_CMD_STOP){			/* 走行停止					*/
			stopflg = true;
		}
		else if(cmd == COMM_CMD_FB){			/* 前後進					*/
			sCtrl.forward = value;
			sCtrl.turn = 0;
		}
		else if(cmd == COMM_CMD_LR){			/* 左右旋回					*/
			sCtrl.turn = value;
		}
		else if(cmd == COMM_CMD_ZERO){			/* 速度ゼロ					*/
			sCtrl.turn = value;
			sCtrl.forward = value;
		}
	}

	/* 状態別処理 */
	switch(sCtrl.runStatus){
		case STATE_CALIB:
			/* ジャイロセンサ値を取得 */
			sCtrl.gyroOffset = ecrobot_get_gyro_sensor(PORT_GYRO);
			/* ジャイロセンサ値をLCDに表示 */
			display_goto_xy(0, 0);
			display_string("Gyro:");
			display_unsigned(sCtrl.gyroOffset, 3);
			display_update();
			stop();
			if(TOUCH_isPressed() == 1){
				beep();
				sCtrl.runStatus = STATE_READY;
			}
			break;
		case STATE_READY:
			balance_init();							/* 倒立振子制御初期化			*/
			nxt_motor_set_count(PORT_WHEEL_L, 0);	/* 左モータエンコーダリセット	*/
			nxt_motor_set_count(PORT_WHEEL_R, 0);	/* 右モータエンコーダリセット	*/
			TAIL_setPosition(TAIL_ANGLE_STAND_UP);
			stop();
			sCtrl.runStatus = STATE_WAIT;
			break;
		case STATE_WAIT:
			if((TOUCH_isPressed() == true) || (startflg == true)){
				beep();
				sCtrl.runStatus = STATE_RUNSTART;
			}
			stop();
			break;
		case STATE_RUNSTART:
			sCtrl.forward = 0;
			sCtrl.turn    = 0;
			TAIL_setPosition(TAIL_ANGLE_DRIVE);
			sCtrl.runStatus = STATE_RUN;
			break;
		case STATE_RUN:
			run();
			if((TOUCH_isPressed() == true) || (stopflg == true)){
				beep();
				TAIL_setPosition(TAIL_ANGLE_STAND_UP - 20);
				sCtrl.gyroOffset -= 100;	/* ジャイロオフセットを操作し、後方に倒す */
				sCtrl.waitTimer = 10;		/* 待ち用タイマに40msをセット */
				sCtrl.runStatus = STATE_STOPREADY;
			}
			break;
		case STATE_STOPREADY:
			sCtrl.forward = 0;
			sCtrl.turn    = 0;
			run();
			if(sCtrl.waitTimer == 0){		/* 待ち用タイママイムアップ */
				sCtrl.gyroOffset += 100;	/* ジャイロオフセットを戻す */
				sCtrl.runStatus = STATE_STOP;
			}
			break;
		case STATE_STOP:
			stop();
			sCtrl.runStatus = STATE_READY;
			break;
		default:
			sCtrl.runStatus = STATE_STOP;
	}

	/* 尻尾制御 */
	TAIL_control();

	/* 100ms計測 */
	sCtrl.count100ms++;
	if(sCtrl.count100ms > 25){
		sCtrl.count100ms = 0;
		/* タッチセンサの押下状態を更新	*/
		TOUCH_updateState();
	}

	/* 待ち用タイマ更新 */
	if(sCtrl.waitTimer > 0){
		sCtrl.waitTimer--;
	}
}

/****************************************************************************/
/* 関数名	: run															*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: 走行する														*/
/****************************************************************************/
static void run(void)
{
	S8 pwm_L;								/* 左モータPWM出力				*/
	S8 pwm_R;								/* 右モータPWM出力				*/

	/* 倒立振子制御 */
	balance_control(
		(float)sCtrl.forward,						/* 前後進命令					*/
		(float)sCtrl.turn,							/* 旋回命令						*/
		(float)ecrobot_get_gyro_sensor(PORT_GYRO),	/* ジャイロセンサ値				*/
		(float)sCtrl.gyroOffset,					/* ジャイロセンサオフセット値	*/
		(float)nxt_motor_get_count(PORT_WHEEL_L),	/* 左モータ回転角度[deg]		*/
		(float)nxt_motor_get_count(PORT_WHEEL_R),	/* 右モータ回転角度[deg]		*/
		(float)ecrobot_get_battery_voltage(),		/* バッテリ電圧[mV]				*/
		&pwm_L,										/* 左モータPWM出力値			*/
		&pwm_R);									/* 右モータPWM出力値			*/

	nxt_motor_set_speed(PORT_WHEEL_L, pwm_L, 1); 	/* 左モータPWM出力セット(-100～100)	*/
	nxt_motor_set_speed(PORT_WHEEL_R, pwm_R, 1); 	/* 右モータPWM出力セット(-100～100)	*/
}

/****************************************************************************/
/* 関数名	: stop															*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: 停止する														*/
/****************************************************************************/
static void stop(void)
{
	/* モータ動作停止 */
	nxt_motor_set_speed(PORT_WHEEL_R, 0, 1);
	nxt_motor_set_speed(PORT_WHEEL_L, 0, 1);
}

/****************************************************************************/
/* 関数名	: beep															*/
/* 引数		: なし															*/
/* 戻り値	: なし															*/
/* 概要		: ビープ音を鳴らす												*/
/****************************************************************************/
static void beep(void)
{
	ecrobot_sound_tone(400, 100, SPEEKER_VOLUME);
}

