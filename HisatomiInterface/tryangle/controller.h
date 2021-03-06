#ifndef _CONTROLLER_H_
#define _CONTROLLER_H_
/****************************************************************************/
/* ファイル名 : controller.h												*/
/* 概要       : 走行コントローラクラスヘッダ								*/
/****************************************************************************/

/****************************************************************************/
/* インクルードファイル														*/
/****************************************************************************/
#include "ecrobot_interface.h"

/****************************************************************************/
/* define定義・マクロ定義													*/
/****************************************************************************/

/****************************************************************************/
/* クラス操作(Public)														*/
/****************************************************************************/
void	CTRL_initializeDevice(void);			/* デバイスを初期化する		*/
void	CTRL_terminateDevice(void);				/* デバイスを終了する		*/
void	CTRL_initialize(void);					/* 初期化する				*/
void	CTRL_execute(void);						/* 制御を実行する			*/

#endif	/* _CONTROLLER_H_ */
