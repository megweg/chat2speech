## Chat To Speech(CTS)

Program what using [BroChat](https://github.com/c0deum/BroChat) to read messages with [Microsoft Speech Platform](https://www.microsoft.com/en-us/download/details.aspx?id=27225).

## What it can do


##### Rules to selecting voice to read
At that time, CTS have only 2 rules, Default(English) and Russian.
**Please, config that before use**

*In future will be created window to create custom rules*


##### Create filter for custom reading of user nickname
On right side of main window located list of users, and you can call create filter by double click on user name.


##### Ignore users
Also use list of users, click mouse left button to select user, and mouse right button to toggle ignore state.

*troubles when nick name started with [*


##### Filter of text of messages
You can set replace for some text to another text, by adding filter.

**Filter have somthing like priorities**

CTS remove smiles and links from message, but yo can replace smile code to another text. You can find then on fully played messages on log.
Also filtering, and not reading messages longer then 200 chars, and messages what contained word longer then 15 chars.

*This feature hardcoded yet*


##### Smart tags for messages
> **+s _mynewname_** 

this tag will add/change reading filter of user nickname

## FAQ

##### How use it ?
You need BroChat with his webserver(this also websocket server used to transfer info to html pages in styles folder) opened on 15619 port (by default on brochat)

##### System Requirements
* Microsoft .NET Framework 4
* Microsoft Speech Platform

##### Where I can find "voices"
* [Microsoft Text-to-Speech engines](https://www.microsoft.com/en-us/download/details.aspx?id=27224) 
HINT: you need tts (text to speech), but not SR (Speech Recognition)
* [VisionAid Voice packages](http://visionaid.com/phpincludes/en/support/voices/voices.php)
HINT: you can install only trial packages for free, but you can find another versions of this packages

If you know other speech engines what working with SAPI5, please add here

