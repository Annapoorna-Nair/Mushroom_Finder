# Mushroom Identification App — Design Document

### Introduction
Have you ever found a mushroom in the woods but were unsure what type it was? Many mushrooms look very similar, even though they may have different characteristics and toxicity levels.
The Mushroom Identification App helps users explore and identify possible mushroom species based on their physical appearance, growing environment, and other characteristics.
The app will help users:
Enter mushroom information and answer questions to identify possible species.
Answer additional questions when more details are needed.
View mushroom matches with pictures, descriptions, and similar-looking species.
Learn about mushroom toxicity, safety warnings, and preparation requirements.
Share pictures, ask questions, and interact with other users in the Mushroom Community.
The app is designed for nature enthusiasts, mushroom collectors, and people interested in learning about edible and poisonous mushroom species.

### Logo
<img src="https://github.com/Annapoorna-Nair/Mushroom_Finder/blob/master/Images/IMG_1233.PNG" alt="Project Logo" width="300">

### Data Feeds
https://www.kaggle.com/datasets/benedictusjason/edible-and-poisonous-mushroom-classification
https://www.kaggle.com/datasets/mdhasanahmad/12-mushroom-species-dataset?resource=download
https://www.kaggle.com/datasets/benedictusjason/edible-and-poisonous-mushroom-classification

### Storyboard
https://canva.link/pbi6jcuj2u5rf6o

### Class diagram
<img src="https://github.com/Annapoorna-Nair/Mushroom_Finder/raw/master/Images/Untitled%20(7).png" alt="Project Logo" width="600">

### Function Requirements 
#### Requirement 100: Enter Mushroom Information
##### Scenario
As a user interested in mushrooms, I want to enter information about a mushroom I found so that the application can search for possible matching species.
##### Dependencies
A mushroom database is available and accessible.
##### Assumptions
Users can recognize some basic mushroom characteristics. Each initial question has predefined answer options.
###### Example
Given the mushroom identification form is available

When I enter the following information:
- Color: Brown
- Location: On wood
- Shape: Rounded
- Width: Medium
- Height: Short
  
Then the application should accept my information and begin searching for possible mushroom matches.

### Requirement 101: Ask Additional Questions
#### Scenario
As a user, I want to answer additional questions when the application cannot sufficiently narrow down the mushroom type, so that I can receive more relevant results.
#### Dependencies
The application has predefined follow-up questions and rules for selecting them.
#### Assumptions
The additional questions are only displayed when the initial answers are insufficient to distinguish between candidate species.
###### Example 1.1
Given I have completed the initial questions

When the application finds several possible matches that cannot be distinguished using my answers

Then the application should display additional questions about the mushroom.
###### Example 1.2
Given additional questions are displayed

When I provide more details about the mushroom's gills and stem

Then the application should update the possible matches based on my new answers.

### Requirement 102: Display Mushroom Safety Notices
#### Scenario
As a user, I want to see information about known mushroom toxicity and preparation risks so that I can better understand potential dangers.
#### Dependencies
The database contains verified references about mushroom toxicity and documented preparation requirements.
#### Assumptions
Mushroom identification results do not establish that a collected specimen is safe to eat.
##### Example 1.1
Given a possible mushroom match is displayed

When the database identifies that species as poisonous

Then the application should display a prominent warning explaining that the species is poisonous and should not be eaten.
##### Example 1.2
Given a possible mushroom match has a documented preparation requirement

When user open its details

Then the application should display that requirement and a warning that cooking does not make an unidentified or poisonous mushroom safe.

### Requirement 103: Mushroom Community (We wait to hear Professor’s thought on this section)
#### Scenario
As a user interested in mushrooms, I want to interact with other users by sharing mushroom pictures, asking questions, and discussing mushroom species so that I can learn from their experiences.
#### Dependencies
The application has a community discussion system where users can create posts, upload pictures, and respond to other users.
#### Assumptions
Users have an account to participate in the community. Community members can share their knowledge and experiences, but their responses are not guaranteed to be accurate.
###### Example 1.1
Given I am logged into the application

When I create a post with a mushroom picture and description

Then other users should be able to view my post and leave comments.
###### Example 1.2
Given another user has posted a question about a mushroom

When I open the post and write a comment

Then my comment should appear under that post for other users to see.
###### Example 1.3
Given I am browsing the Mushroom Community

When I select a discussion post

Then I should be able to view its pictures, descriptions, and comments from other users.

### Team Composition
Annapoorna Nair

Sara Bahojb Ghods

Chau Trinh 

### Weekly Meeting 
Once a week and every Thursday at 1:30pm. 

