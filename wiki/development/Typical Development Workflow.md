This page describes how development takes place here!
# Overview
* This project uses **weekly** sprints/iterations. Sprints are when code changes can happen and start on **Wednesday** and end on **Tuesday**
* Changes occur through **pull requests only**. This means you cannot push straight to `main`. Instead, you will create a `git` branch and make changes there. This repository automatically creates a pull request for you :)
* Work should be organised via **GitHub Issues**. This means that people should assign themselves to an issue before starting work. This is to prevent 2 people from working on the same thing!
* Currently, Emil (@ehharvey) is the one creating issues and managing the direction of the project. As time goes on, this could change!
# Process steps
Below is a table listing what people should do each week :)

| Day(s)                        | Instructions                                                                                                                                                                                                                                                                                  |
| ----------------------------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Sunday                        | Emil will finalize the work needed for next week.<br><br>(Other people can offer suggestions! Anyone can make an issue or reach out to Emil over Slack or the GitHub discussions)                                                                                                             |
| Monday<br>Tuesday             | Folks should look over the issues created and assign themselves to the ones they want to complete! Also be sure to use the comments to discuss.<br><br>Some issues are an 'everyone' issue, meaning that everyone will have identical work to complete. There will be 1 sub-issue per person. |
| Wednesday                     | Iteration starts! People should create a **branch** for their issue. This can be done from the Issue web page or via `git`. All branches should be based off `main`<br><br>See [[How to complete issues]] for step-by-step instructions on how to do this!                                    |
| Thursday-Friday               | Be sure to reach out if you are stuck or need help!<br><br>You can reach out by making a comment on your issue or on Slack.                                                                                                                                                                   |
| Saturday                      | Emil will check-in by this day. He will see if any issues need to be reassigned, etc.                                                                                                                                                                                                         |
| Sunday                        | Work for the next week becomes finalized! Folks can then assign themselves to the work they want to do.                                                                                                                                                                                       |
| Monday or Tuesday (next week) | Emil will host/post a retrospective/update. This could be a meeting or just a video that Emil posts :)                                                                                                                                                                                        |
## Coding Standards
To maintain good project health, this project has several coding standards:
1. Code changes should have accompanying tests to verify them
2. Code changes should have accompanying documentations (on this wiki). Things to include are:
	1. Online references used, like instructions and advice
	2. Challenges faced
	3. New processes (e.g., to test, build, etc.)
3. Pull requests should:
	1. Be linked to the relevant Issue (do so in the description of the pull request!)
	2. Describe the changes made
	3. Be as small as possible (a pull request should only target one issue)