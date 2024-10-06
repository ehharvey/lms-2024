---
date: 2024-09-30
title: Iteration 2 starting soon!
---
Hi, everyone!

Thank you all for all the PRs coming in! It is looking great so far!

I'll make a video re: this announcement on Tuesday October 1, so if you want to wait until then, feel free to do so :)

I'm partway through getting Issues created for iteration 2. This project will begin as a student-focused app, but we will for sure explore teacher-related use cases in the coming weeks. For this iteration, I planned for us to build some task-tracking type functionality.

As an overview:
1. I completed #48 and #22 , giving us a basic console app and test pipeline. Can't wait to see your tests!
2. The project direction is towards a **student-focused** app. This week will focus on task-tracking features. I've made descriptions for Work Item related work.
3. I expect this week to lead to some bad code quality (e.g., duplication). I think this is OK; let's tackle that next week!
4. Remember, our next iteration starts **Wednesday, October 2**. Please assign yourself to items you'd like to complete!

# Updates

## CLI
I completed #48. This adds super basic CLI (command-line interface) functionality for this project. At a super high level, the app works with a `noun verb` pattern. For example:
```
$ dotnet run credits list
Credits
-------
Emil Harvey
Parth Gajjar
Boa Im
Nimeshkumar Chaudhari
Daphne Duong
```

I would like everyone to follow the same pattern moving forward! I'll try to document this on the wiki (tracked by #95).

## Testing!
I've completed #22 , which gives us an automated test pipeline. Moving forward, people should try to make tests for the Issue they complete. This can be challenging for a console app. My recommendation is to split work up into functions. You can have 1 function that basically handles the console side of things and other functions that do the work.

# New Issues
I've created a host of Issues for all of us to explore :). These focused on task tracking features. In particular, I've thought of **Work Items**, **Progress Updates**, and **Blocks** as types of data to think about:
* A Work Item is something that a student needs to complete, like a homework assignment
* A Progress Update is something where the student can record progress made (usually against a Work Item)
* A Block is something that prevents the student from making progress. For example, maybe they need to learn/review a topic or maybe the assignment has not been released yet.

For this iteration, I am taking responsibility of the data modelling. I have not merged these changes yet, but you can take a look at #94 (look at files changed). This PR will hold the data models that we will use this iteration.

I also have not finished filling out the Issue descriptions. I _have_ done so for the Work Item related Issues. I expect the others to be similar, but I will get to them on Tuesday (Oct 1).

**I expect the work listed currently to lead into some bad code :( .** As I note on #81 , the code to list work items probably will be similar to code listing blocks or progresses. I don't want to over obsess on code quality just yet. Instead, I'll plan for us to do some refactoring next week :) .

# Existing Issues
Some of us still need to complete the wiki/credits Issue. I'll leave these open and let people complete them whenever. I hope the Issue page doesn't get too crowded. Also remember that I have created a personal kanban board for all of us [here](https://github.com/users/ehharvey/projects/13/views/5). There is also a tab [there](https://github.com/users/ehharvey/projects/13/views/6) to look at unassigned work!

# Next Steps
Our next iteration starts **Wednesday, October 2**. Until then, feel free to look over the work and assign yourself to items you would like to complete!
