@Quiz @QuizEditing
Feature: Editing Name of Quiz 

	Scenario: Editing the name of an existing quiz
		Given a new quiz in the list 
		When clicking on the edit icon in the quiz card 
		And clicking on the edit icon in the action ribbon in the quiz view
		And writing "Football" in the edit field of the quiz edit window
		And pressing the confirm button on the quiz edit window 
		Then quiz name in the header title should show "Football"