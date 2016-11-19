/****************************************************************************/
/* �t�@�C���� : controller.c												*/
/* �T�v       : ���s�R���g���[���N���X										*/
/****************************************************************************/

/****************************************************************************/
/* �C���N���[�h�t�@�C��														*/
/****************************************************************************/
#include "kernel.h"
#include "kernel_id.h"
#include "system.h"
#include "controller.h"
#include "touch.h"
#include "tail.h"
#include "communication.h"
#include "balancer.h"						/* �|���U�q����p�w�b�_�t�@�C�� */

/****************************************************************************/
/* define��`�E�}�N����`													*/
/****************************************************************************/

/****************************************************************************/
/* �N���X����																*/
/****************************************************************************/
typedef enum {								/* ���s���						*/
	STATE_CALIB,							/*   �L�����u���[�V������		*/
	STATE_READY,							/*   ������						*/
	STATE_WAIT,								/*   ���s�҂�					*/
	STATE_RUNSTART,							/*   ���s�J�n					*/
	STATE_RUN,								/*   ���s��						*/
	STATE_STOPREADY,						/*   ��~����					*/
	STATE_STOP								/*   ��~��						*/
} eRunState;

typedef struct {
	eRunState	runStatus;					/* ���s���						*/
	U16		count100ms;						/* 100ms�v���J�E���^			*/
	U16		waitTimer;						/* �҂��p�^�C�}�i4ms���Ɍ��Z�j	*/
	U16		gyroOffset;						/* �W���C���I�t�Z�b�g			*/
	S8		forward;						/* �O��i����					*/
											/* (-100�`+100 +:�O/-:��)		*/
	S8		turn;							/* ���񖽗�						*/
											/* (-100�`+100 +:�E/-:��)		*/
} CTRL;

/****************************************************************************/
/* �N���X����(Private)														*/
/****************************************************************************/
static void run(void);						/* ���s����						*/
static void stop(void);						/* ��~����						*/
static void beep(void);						/* �r�[�v����炷				*/

/****************************************************************************/
/* �C���X�^���X																*/
/****************************************************************************/
static CTRL		sCtrl;						/* ���s����						*/


/****************************************************************************/
/* �֐���	: CTRL_initializeDevice											*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: �f�o�C�X������������											*/
/****************************************************************************/
void CTRL_initializeDevice(void)
{
	/* ��~ */
	stop();

	/* �ʐM�f�o�C�X�̏����� */
	COMM_initializeDevice();
}

/****************************************************************************/
/* �֐���	: CTRL_terminateDevice											*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: �f�o�C�X���I������											*/
/****************************************************************************/
void CTRL_terminateDevice(void)
{
	/* �ʐM�f�o�C�X�̏I�� */
	COMM_terminateDevice();
}

/****************************************************************************/
/* �֐���	: CTRL_initialize												*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: ����������													*/
/****************************************************************************/
void CTRL_initialize(void)
{
	TOUCH_initialize();						/* �^�b�`�Z���T�̏�����			*/
	COMM_initialize();						/* �ʐM�̏�����					*/
	TAIL_initialize();						/* �K���̏�����					*/

	sCtrl.count100ms = 0;
	sCtrl.waitTimer  = 0;
	sCtrl.forward = 0;
	sCtrl.turn    = 0;
	sCtrl.runStatus = STATE_CALIB;
}

/****************************************************************************/
/* �֐���	: CTRL_execute													*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: ��������s����												*/
/* ���L����	: 4ms�Ԋu�Ŏ��s���邱��											*/
/****************************************************************************/
void CTRL_execute(void)
{
	S8	startflg = false;
	S8	stopflg  = false;
	S8	cmd;
	S8	value;

	/* �ʐM��M�`�F�b�N */
	if(COMM_isReceived() == 1){
		COMM_getCmdInfo(&cmd, &value);
		if(cmd == COMM_CMD_START){				/* ���s�J�n					*/
			startflg = true;
		}
		else if(cmd == COMM_CMD_STOP){			/* ���s��~					*/
			stopflg = true;
		}
		else if(cmd == COMM_CMD_FB){			/* �O��i					*/
			sCtrl.forward = value;
			sCtrl.turn = 0;
		}
		else if(cmd == COMM_CMD_LR){			/* ���E����					*/
			sCtrl.turn = value;
		}
		else if(cmd == COMM_CMD_ZERO){			/* ���x�[��					*/
			sCtrl.turn = value;
			sCtrl.forward = value;
		}
	}

	/* ��ԕʏ��� */
	switch(sCtrl.runStatus){
		case STATE_CALIB:
			/* �W���C���Z���T�l���擾 */
			sCtrl.gyroOffset = ecrobot_get_gyro_sensor(PORT_GYRO);
			/* �W���C���Z���T�l��LCD�ɕ\�� */
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
			balance_init();							/* �|���U�q���䏉����			*/
			nxt_motor_set_count(PORT_WHEEL_L, 0);	/* �����[�^�G���R�[�_���Z�b�g	*/
			nxt_motor_set_count(PORT_WHEEL_R, 0);	/* �E���[�^�G���R�[�_���Z�b�g	*/
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
				sCtrl.gyroOffset -= 100;	/* �W���C���I�t�Z�b�g�𑀍삵�A����ɓ|�� */
				sCtrl.waitTimer = 10;		/* �҂��p�^�C�}��40ms���Z�b�g */
				sCtrl.runStatus = STATE_STOPREADY;
			}
			break;
		case STATE_STOPREADY:
			sCtrl.forward = 0;
			sCtrl.turn    = 0;
			run();
			if(sCtrl.waitTimer == 0){		/* �҂��p�^�C�}�}�C���A�b�v */
				sCtrl.gyroOffset += 100;	/* �W���C���I�t�Z�b�g��߂� */
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

	/* �K������ */
	TAIL_control();

	/* 100ms�v�� */
	sCtrl.count100ms++;
	if(sCtrl.count100ms > 25){
		sCtrl.count100ms = 0;
		/* �^�b�`�Z���T�̉�����Ԃ��X�V	*/
		TOUCH_updateState();
	}

	/* �҂��p�^�C�}�X�V */
	if(sCtrl.waitTimer > 0){
		sCtrl.waitTimer--;
	}
}

/****************************************************************************/
/* �֐���	: run															*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: ���s����														*/
/****************************************************************************/
static void run(void)
{
	S8 pwm_L;								/* �����[�^PWM�o��				*/
	S8 pwm_R;								/* �E���[�^PWM�o��				*/

	/* �|���U�q���� */
	balance_control(
		(float)sCtrl.forward,						/* �O��i����					*/
		(float)sCtrl.turn,							/* ���񖽗�						*/
		(float)ecrobot_get_gyro_sensor(PORT_GYRO),	/* �W���C���Z���T�l				*/
		(float)sCtrl.gyroOffset,					/* �W���C���Z���T�I�t�Z�b�g�l	*/
		(float)nxt_motor_get_count(PORT_WHEEL_L),	/* �����[�^��]�p�x[deg]		*/
		(float)nxt_motor_get_count(PORT_WHEEL_R),	/* �E���[�^��]�p�x[deg]		*/
		(float)ecrobot_get_battery_voltage(),		/* �o�b�e���d��[mV]				*/
		&pwm_L,										/* �����[�^PWM�o�͒l			*/
		&pwm_R);									/* �E���[�^PWM�o�͒l			*/

	nxt_motor_set_speed(PORT_WHEEL_L, pwm_L, 1); 	/* �����[�^PWM�o�̓Z�b�g(-100�`100)	*/
	nxt_motor_set_speed(PORT_WHEEL_R, pwm_R, 1); 	/* �E���[�^PWM�o�̓Z�b�g(-100�`100)	*/
}

/****************************************************************************/
/* �֐���	: stop															*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: ��~����														*/
/****************************************************************************/
static void stop(void)
{
	/* ���[�^�����~ */
	nxt_motor_set_speed(PORT_WHEEL_R, 0, 1);
	nxt_motor_set_speed(PORT_WHEEL_L, 0, 1);
}

/****************************************************************************/
/* �֐���	: beep															*/
/* ����		: �Ȃ�															*/
/* �߂�l	: �Ȃ�															*/
/* �T�v		: �r�[�v����炷												*/
/****************************************************************************/
static void beep(void)
{
	ecrobot_sound_tone(400, 100, SPEEKER_VOLUME);
}

