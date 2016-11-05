/****************************************************************************/
/* �t�@�C���� : communication.c												*/
/* �T�v       : �ʐM�N���X													*/
/****************************************************************************/

/****************************************************************************/
/* �C���N���[�h�t�@�C��														*/
/****************************************************************************/
#include "kernel.h"
#include "kernel_id.h"
#include "system.h"
#include "communication.h"
#include <string.h>

/****************************************************************************/
/* define��`�E�}�N����`													*/
/****************************************************************************/
#define COMM_MAX_SIZE		(16)


/****************************************************************************/
/* �N���X����																*/
/****************************************************************************/
typedef enum {								/* ��M�f�[�^��͏��			*/
	STATE_WAIT,								/*   �J�n��M�҂�				*/
	STATE_CMD,								/*   �R�}���h��M��				*/
} eRxState;

typedef struct {
	S8		btRxBuff[BT_MAX_RX_BUF_SIZE];	/* Bluetooth�p��M�o�b�t�@		*/
	S8		rxFrameData[COMM_MAX_SIZE];		/* ��M�f�[�^�o�b�t�@			*/
	S8		rxFrameSize;					/* ��M�f�[�^�T�C�Y				*/
	S8		cmd;							/* ��M�R�}���h���				*/
	S8		value;							/* ��M�R�}���h�f�[�^�l			*/
	eRxState	rxStatus;					/* ��M�f�[�^��͏��			*/
} COMM;



/****************************************************************************/
/* �N���X����(Private)														*/
/****************************************************************************/
static int analyzeRxData(void);				/* ��M�f�[�^����͂���			*/


/****************************************************************************/
/* �C���X�^���X																*/
/****************************************************************************/
static COMM		sComm;						/* �ʐM							*/


/****************************************************************************/
/* �֐���	: COMM_initializeDevice											*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: �f�o�C�X������������											*/
/****************************************************************************/
void COMM_initializeDevice(void)
{
	/* Bluetooth�f�o�C�X�̏����� */
	ecrobot_init_bt_slave(PASS_KEY);
}

/****************************************************************************/
/* �֐���	: COMM_terminate												*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: �f�o�C�X���I������											*/
/****************************************************************************/
void COMM_terminateDevice(void)
{
	/* Bluetooth�ʐM�I�� */
	ecrobot_term_bt_connection();
}

/****************************************************************************/
/* �֐���	: COMM_initialize												*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: ����������													*/
/****************************************************************************/
void COMM_initialize(void)
{
	/* Bluetooth�f�o�C�X���̐ݒ� */
	ecrobot_set_bt_device_name(DEVICE_NAME);

	sComm.cmd = COMM_CMD_STOP;
	sComm.value = 0;
	sComm.rxStatus = STATE_WAIT;
}

/****************************************************************************/
/* �֐���	: COMM_isReceived												*/
/* ����		: �Ȃ�															*/
/* �߂�l	: ��M����														*/
/*          :   0 : ��M�f�[�^����											*/
/*          :   1 : ��M�f�[�^�L��											*/
/* �T�v		: ��M�f�[�^�L�����擾����										*/
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
						/* �R�}���h��M���� */
						sComm.rxFrameData[sComm.rxFrameSize] = '\0';
#if 1
						/* �f�o�b�O�p�Ɏ�M�f�[�^��LCD�ɕ\�� */
						display_clear(0);
						display_goto_xy(0, 1);
						display_string((const char*)sComm.rxFrameData);
						display_update();
#endif
						/* ��M�f�[�^����� */
						if(analyzeRxData() == 1){
							/* �L���ȃf�[�^�̏ꍇ�̓��X�|���X�𑗐M */
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
/* �֐���	: COMM_getCmdInfo												*/
/* ����		: cmd   : �R�}���h												*/
/*			: value : �l													*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: �R�}���h�����擾����										*/
/****************************************************************************/
void COMM_getCmdInfo(S8 *cmd, S8 *value)
{
	*cmd = sComm.cmd;
	*value = sComm.value;
}

/****************************************************************************/
/* �֐���	: analyzeRxData													*/
/* ����		: �Ȃ�															*/
/* �߂�l	: ��͌���														*/
/*          :   0 : �L���f�[�^����											*/
/*          :   1 : �L���f�[�^�L��											*/
/* �T�v		: ��M�f�[�^����͂���											*/
/*			:																*/
/*			:	 <START>	���s�J�n										*/
/*			:	 <STOP>		���s��~										*/
/*			:	 <Fxxx>		�O�i(xxx:0�`100)								*/
/*			:	 <Bxxx>		��i(xxx:0�`100)								*/
/*			:	 <Rxxx>		�E����(xxx:0�`100)								*/
/*			:	 <Lxxx>		������(xxx:0�`100)								*/
/****************************************************************************/
static int analyzeRxData(void)
{
	int	ret = 0;							/* ��͌���						*/
	int	value;								/* �f�[�^�l						*/

	/* ���s�J�n�`�F�b�N */
	if(strcmp("<START>", (const char*)sComm.rxFrameData) == 0){
		sComm.cmd = COMM_CMD_START;
		ret = 1;
	}
	/* ���s��~�`�F�b�N */
	else if(strcmp("<STOP>", (const char*)sComm.rxFrameData) == 0){
		sComm.cmd = COMM_CMD_STOP;
		ret = 1;
	}
	/* �O��i�E����`�F�b�N */
	else {
		/* ���������̎擾 */
		value = 0;
		if((sComm.rxFrameSize - 3) == 1){			/* ����������1�� */
			value = sComm.rxFrameData[2] - '0';
		}
		else if((sComm.rxFrameSize - 3) == 2){		/* ����������2�� */
			value = ((sComm.rxFrameData[2] - '0') * 10)
				+  (sComm.rxFrameData[3] - '0');
		}
		else if((sComm.rxFrameSize - 3) == 3){		/* ����������3�� */
			value= ((sComm.rxFrameData[2] - '0') * 100)
				+ ((sComm.rxFrameData[3] - '0') * 10)
				+  (sComm.rxFrameData[4] - '0');
		}
		/* �ŏ��l/�ő�l�`�F�b�N */
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
