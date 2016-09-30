#ifndef _SYSTEM_H_
#define _SYSTEM_H_
/****************************************************************************/
/* ファイル名 : system.h													*/
/* 概要       : システム共通ヘッダ											*/
/****************************************************************************/

/****************************************************************************/
/* define定義・マクロ定義													*/
/****************************************************************************/
/* 接続ポート */
#define PORT_WHEEL_R	NXT_PORT_B			/* 右車輪用モータ接続ポート		*/
#define PORT_WHEEL_L	NXT_PORT_C			/* 左車輪用モータ接続ポート		*/
#define PORT_TAIL		NXT_PORT_A			/* 尻尾用モータ接続ポート		*/
#define PORT_GYRO		NXT_PORT_S1			/* ジャイロセンサ接続ポート		*/
#define PORT_SONAR		NXT_PORT_S2			/* ソナーセンサ接続ポート		*/
#define PORT_LIGHT		NXT_PORT_S3			/* 光センサ接続ポート			*/
#define PORT_TOUCH		NXT_PORT_S4			/* タッチセンサ接続ポート		*/

/* Speeker */
#define SPEEKER_VOLUME		(20)			/* スピーカーの音量				*/

/* Bluetooth */
#define PASS_KEY		"1234"				/* Bluetoothパスキー交換用ピンコード(最大16文字) */
#define DEVICE_NAME		"TRYANGLE"			/* Bluetoothデバイス名			*/

#endif	/* _SYSTEM_H_ */
