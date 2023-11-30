For the actual game itself. you only need 3 folders

Project settings
Assests
Packages

Every other folder can be deleted. It will be regenerated when you open the project on your set up. 
The unity version for this project is 2021.3.17f1, do not under any circumstances change the unity version of the project. 
https://unity.com/releases/editor/whats-new/2021.3.17
Link to the webpage to download the version. ^^ Any version of Unity hub will work so don't worry about that. 
All it does is create headache for the programmers. 

Update: I just created a gitignore, it got annoying have to delete a gb and have the project reload every single time. If you want to add a 
a directory(folder) to the gitignore. just type the name of the folder with this slash afterwards/ and you'll be good to go. you can also 
have gitignore ignore certain file types or just specific files as well. 
The links below may help if my explanation is confusing. 
https://stackoverflow.com/questions/343646/ignoring-directories-in-git-repositories-on-windows
https://www.atlassian.com/git/tutorials/saving-changes/gitignore
https://www.freecodecamp.org/news/gitignore-file-how-to-ignore-files-and-folders-in-git/#:~:text=The%20types%20of%20files%20you,the%20same%20project%20as%20you.

How to create a gitignore file
https://docs.github.com/en/get-started/getting-started-with-git/ignoring-files
(touch is a gitbash command that literally creates a file.)

How to use git

Commit- when you make changes within the repository(or project foler) you can make these changes permanent by committing them to the branch.
This will create a version of the project that has your new changes, while your previous changes are kept in an earlier version of the branch.

Push- you've committed your changes, but you don't see the changes reflected on your remote(cloud) repository.
You need to "push" them into the branch. These are the changes you want to see actual, so push them so the branch receives them. 

Pull- similar to the previously mentioned push, instead of "pushing" changes to your remote repository. You are pulling them from the cloud 
and putting them on your local machine. You are downloading changes or updates of your repository. 

GitHub WorkFlow
There is the original main branch which always has a working version of our game. It's called the main branch. 
As you already know, github tracks your changes to the files. That's the reason why github allows everyone to upload repositories because tracking
your changes does not take up a lot of space. 
The main branch is sacred, and should never be edited directly. What if you mess everything up and the game doesn't work?
That's why we work on a working copy of the main branch. This branch is entirely separate from the main branch
which allows us to work without any worry of messing with the perfect, sacred main branch. 

When you create a new branch, be sure to publish that branch. If not published, that branch only exists on your local computer. 
Not in the cloud where others can view and edit. 

New branches is named after a pokemon, I go by the number of branches. So if we've been through 4 branches,
 then the fifth branch will be Wartortle as Charmeleon's number in the national pokedex is five. 
When you've made a great change and it does not break the game. You push your changes to that branch.
After, you want to make a pull request.
A pull request, requests the pulls(your new changes)from one branch into another.
Occansionally, you may run into merge conflicts, when one file and another won't work together properly. 
For the purposes of this class we shouldn't have any but if you do feel free to send me a message and I'll take a look at it within the hour.
If there are no merge conflicts, you can compare the changes from previous versions of the file. 
Assuming that everything is all great, you can complete your pull request and successfully merge the two branches.
After you pull and merge the changes, github will give you the option to delete the branch. As a programmer I recommend you to delete the branch.
After you delete the branch, create a new branch to work on. Remember, your branch and my branch are to be separate until we pull and merge into main.

This is a basic overview to git. I might list some terminal commands if you're interested or if something get's really bad, but we're not going to worry about that for now. 