---
title: Overview
date: 2024-10-08
---
This page provides an overview of this project, specifically in regards to its topic and direction.
# Summary
1. This project aims to be a learning management system like D2L or BlackBoard
2. The emphasis of this project is less on the topic and more on the process of development and operations
3. The initial stages of the project will produce a CLI; later stages will produce and maintain a running web application
# Topic
LMS-2024 will be a learning management system. Please know that the emphasis of this project is not on the topic! A learning management system was chosen to give something that is already familiar for many people.

In addition, remember that software projects evolve. Grow comfortable with changing scope and direction. LMS-2024 uses weekly sprints, giving us a frequent opportunities to reflect and course correct.
## Description
With that in mind, here is a general project description:
### Users
1. Learner
	1. Will submit things
	2. View learning materials
	3. Report progress
2. Teacher
	1. Creates learning materials
	2. Grades submissions
	3. etc.
3. (maybe others too, e.g., graders, etc.)
### Features
1. CRUD learning materials
2. CRUD submissions
3. CRUD gradings
## Goals
Since LMS-2024 emphasises the process, here is what we should all focus on when contributing to this project!
### Technical
#### Code and Dependency Management
This refers to managing our very own software project. We will start with a basic C# application with minimal dependencies (just EF Core). As we develop, this list of dependencies will grow! We will transition to ASP.net, for example. Later on, we will be managing both a C# web server and a React frontend!

Some of you may be familiar using with Visual Studio to manage C# applications. While Visual Studio is a useful tool, it does hide some of the processes that is helpful to know. It's great to know this when things break or if you're working on a project that Visual Studio doesn't (which is a *lot* of projects). (Also, Emil runs Linux so he's not able to use Visual Studio easily)
#### Maintaining Code
Software engineering is sometimes described as [programming integrated over time](https://adamj.eu/tech/2021/11/03/software-engineering-is-programming-integrated-over-time/). This refers to building code over a long period of time. This activity presents many challenges! We often need to balance immediate goals with long-term practices. We don't want to obsess over producing the *best possible architecture* if it means we get nothing done! Meanwhile, we don't want to write mess that becomes impossible to maintain later.
#### Understanding Code
A common mistake that software engineers make is trying to understand an entire codebase. This is impossible. Instead, we will want to learn how to look at code in a smart way. We want to know how we want to look at code, which often includes thinking about what we need to understand and what are safe assumptions to make. This comes with time and experience. This project hopes to start small and grow, which should *hopefully* give a better experience than trying to contribute to an already massive project.
#### Git
Arguably `git` is both a technical and nontechnical skill. While there are a lot of helpful tools to use `git`, the you'll gain a lot by using it over a longer period of time.
#### Operations
Later in this project, we'll be launching our project on the web! This means having a ~24x7 service that we will need to maintain. We'll learn about how deployment happens, how we monitor services, and how we can upgrade a service while it is live!
### Nontechnical
#### Working in Teams
If we were to revise the whole "software engineering = programming integrated over time", we might append "and people" to the end of that phrase. Since you won't be able to know the *entire* codebase at once, you will need to work with others. Teamwork includes thinking about how your work relates to others, knowing how to ask questions, and communication skills!
#### Project Management
You don't need to get involved with the project management aspect too heavily, but you can get a small taste of it while contributing to this project. [Self-management](https://tech.target.com/blog/self-management-career) is an important skill for software engineers, so even if you don't have the team lead cap on, you will still benefit from knowing how to manage.
#### Communication
Communication is a separate heading because it's pretty darn important. As we collaborate, write code, and generally *work*, you'll grow your communication skills. This will include written and verbal communication, code quality and documentation, and more!
# Timings
Here, this page describes timing-related information for this project.
## Iterations
This project will use weekly iterations starting on Wednesdays. Emil will try to post work on the Monday/Tuesday. On Wednesday, folks can assign themselves to work they want to complete! See [[Typical Development Workflow]] for more information.
## Timeline
This project aims to create monthly releases. This means creating a somewhat finalised release at the end of a month. Releases will use a `year-month` format using the month of the next month. So, for all of the work completed in October, a release will created called `2024-11`.

As time goes on, we will plan our releases with more focus. In the beginning, however, we are mostly worried about planning for the next week.

Here is a rough timeline, though note that this will likely change as time goes on.
### 2024-11
* Basic CLI
	* Student-focused
	* Basic assignment, progress tracking
	* Nice-to-have: teacher functionality
		* Grades manager
		* User management (to switch between student/teacher)
* Operations-side
	* Code coverage recording
	* Automated testing
	* Automated releases
	* Installers (Windows, Mac, Linux)
### 2024-12
* (I'm mindful that this is exam season for folks, so I'm intentionally setting our scope small here :))
* Transition to web server
	* Unix-socket (essentially localhost) service
	* CLI transitions to interact against service
* Additional functionality
	* Logins?
	* Assign work
	* Submit work
	* Grade work
* Operations
	* Baremetal install
	* Docker
### 2024-01
* Web frontend
* Operations
	* K8s