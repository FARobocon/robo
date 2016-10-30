//----------------------------------------------------
// sing.c written by inaya and manome  (Triangle Team)
//----------------------------------------------------
//Header
#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>

//Macro
#define N 256
#define SUCCESS 1
#define SLEEP_TIME_DISCONNECTED 3
#define DISCONNECTED 0
#define CONNECT_SUCCESS 1

// Command definitions
#define COMMAND_SING_STR  "<SING>"
#define COMMAND_SING 100
#define COMMAND_DANCE_STR  "<DANCE>"
#define COMMAND_DANCE 200
#define COMMAND_UNKNOWN 9999	// Unknown command
#define COMMAND_EOF -1		// EOF or Connection closed

//Variables
FILE *fp;
char *filename = "/dev/rfcomm0";
char readline[N] = { '\0' };

//Functions
int setup_connection();
int close_connection();
void wait_connection();
int get_command();
void handle_command(int);
void sing();
void dance();

/*********************************************
 * main
 *
 *
 *********************************************/
int main(void)
{
    int command;

    while (1) {
	if (setup_connection() == CONNECT_SUCCESS) {
	    while (1) {
		command = get_command();
		if (command != COMMAND_EOF) {
		    handle_command(command);
		} else {
		    close_connection();
		    break;
		}
	    }
	}
	wait_connection();
    }

    return 0;
}


/**********************************************
 * Setup bluetooth serial connection
 *
 * Returns: DISCONNECTED(0), CONNECT_SUCCESS(1)
 **********************************************/
int setup_connection()
{
    /* open the bluetooth device filename        */
    if ((fp = fopen(filename, "r")) == NULL) {
	return DISCONNECTED;
    }

    return CONNECT_SUCCESS;
}

/**********************************************
 * Close bluetooth serial connection
 *
 * Returns: SUCCESS(0) ERROR(-1)
 **********************************************/
int close_connection()
{
    /* open the bluetooth device filename        */
    return fclose(fp);
}

/**********************************************
 * Wait for serial connection
 *
 *
 **********************************************/
void wait_connection()
{
    fprintf(stderr, "Bluetooth serial client is not connected.\n");
    fprintf(stderr, "Sleeping Zzz...\n");
    sleep(SLEEP_TIME_DISCONNECTED);

    return;
}


/**********************************************
 * get_command
 *
 * Returns:
 *      SUCCESS: 
 *          COMMAND_XXX (XXX)
 *      ERROR
 *          COMMAND_UNKNOWN (9999)
 *      EOF or DISCONNECTED
 *          COMMAND_EOF(-1)
 **********************************************/
int get_command()
{
    if (fgets(readline, N, fp) != NULL) {
	if (strstr(readline, COMMAND_SING_STR) != NULL) {
	    return COMMAND_SING;
	}
	if (strstr(readline, COMMAND_DANCE_STR) != NULL) {
	    return COMMAND_DANCE;
	}
	return COMMAND_UNKNOWN;
    }

    return COMMAND_EOF;
}

/**********************************************
 * Command handler
 *
 **********************************************/
void handle_command(int command)
{
    switch (command) {
    case COMMAND_SING:
	sing();
	break;
    case COMMAND_DANCE:
	dance();
	break;
    default:
	break;
    }

    return;
}

/**********************************************
 * sing
 *
 *
 **********************************************/
void sing()
{
    //aplay “I‚È
    fprintf(stderr, "enter sing()");
    system("/usr/bin/aplay /home/pi/git/Shovelcar/music/test.wav");
    fprintf(stderr, "exit sing()");
}

/**********************************************
 * dance
 *
 *
 **********************************************/
void dance()
{
    fprintf(stderr, "enter dance()");
    system("/home/pi/git/Shovelcar/scripts/danceA.sh");
    fprintf(stderr, "exit dance()");
}
