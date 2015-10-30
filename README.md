# YouDecideAPI
HackManchester 2015 Clockwork challenge!
LateRooms team - Clare Sudbery, Laurent Humbert, Danny Parkinson

The phone number you have to send texts to:

	+44 7860 033 661
	
Continuous deployment - AppHarbor:

	https://appharbor.com/applications/youdecideapi
	
Connection strings:

	Server: mongodb://appharbor_5tjq291m:kkj4e5ighno0r7cl58em1u7q0a@ds041494.mongolab.com:41494/appharbor_5tjq291m
	
	Local: mongodb://localhost:27017/test
	
Git:

	Front end: https://github.com/lhbt/YouDecide
	
	API: https://github.com/lhbt/YouDecideApi
	
Live:

	API: http://youdecideapi.apphb.com/inputsms?content=start&from=07762637697
	
	Front end: http://52.18.191.88/
	
Local (needs running instance of Mongo! Run this from command line: C:\mongodb\bin\mongod.exe):

	API Start: http://youdecide.local/inputsms?content=start&from=447762637697
	
	API Option: http://youdecide.local/inputsms?content=2&from=447762637697
	
	Front end: 
	
		http://localhost:8081/
		
		or http://localhost:8081/#447762637697
		
			(Currently I'm on port 8081 - but it won't work unless you run the http-server command first)
			
			(see below for more detailed instructions)
			
		To get front end up to get running:
		
		At the command line, run 
		
			npm install -g http-server
			
			[go to folder where front end lives]
			
				eg cd C:\Development\YouDecide
				
			http-server	
			
				IF you get an error saying EADDRINUSE, it means you already have something running on port 8080
				
				In this case, set a specific port number like this:
				
				http-server.cmd -p 8081
				
		Now just navigate to  http://localhost:8080	
		
			or if you used a different port in the instructions above, replace 8080 with your new port number
			
Find Sailor Moon:

	In the API code base: YouDecideAPI/sailor_moon_navigation.html
	
	(on Clare's machine: file:///C:/Development/YouDecideAPI/YouDecideAPI/sailor_moon_navigation.html)
	
Youtube:

	Interview with team: https://www.youtube.com/watch?v=1A-LTFmmF8w
	
	Our submission: https://www.youtube.com/watch?v=Mr9mQh6i3sQ&feature=youtu.be
	
Code bases on Clare's machine:

	API: C:\Development\YouDecideAPI
	
	Front end: C:\Development\YouDecide
	
Mongo:

	•	https://docs.mongodb.org/getting-started/csharp/client/
	
	•	https://docs.mongodb.org/v3.0/tutorial/install-mongodb-on-windows/
	
	•	http://geekswithblogs.net/renso/archive/2009/10/21/how-to-set-the-windows-path-in-windows-7.aspx
	
	•	https://docs.mongodb.org/manual/applications/data-models-tree-structures/
	
Starting your Mongo instance:

	Command line: C:\mongodb\bin\mongod.exe
	
Deploying the front end from the command line:

	npm install -g http-server
	
	npm install -g gulp
	
	[Go to root of source folder]
	
	[you'll need to put a private ssh key here: C:\Users\csudbery.ssh]
	
		If the above path is wrong, by running gulp deploy you should get a message telling you where to put the key
		
		Clare has a key here: C:\Development\YouDecide\awskey.pem
		
	gulp deploy
	
		If you get errors like "Cannot find module 'gulp-scp2'" then install the specified component
		
			eg ""npm install gulp-scp2"
			
		This runs gulpfile.js, which will be in source root folder
		
	simply drop the file in /YouDecide/
	
Importing the db locally from the command line:

	c:\mongodb\bin\mongoimport --db test --collection MasterStory_mongo --drop --file MasterStory_mongo.json
