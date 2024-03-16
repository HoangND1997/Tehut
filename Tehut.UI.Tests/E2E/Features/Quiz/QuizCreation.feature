@Quiz @QuizCreation
Feature: Creation of Quizzes

	Scenario: Create quiz by clicking the add card element
		When clicking on the add-quiz card element 
		Then a new quiz is added to the quiz list 

	Scenario: Create quiz by clicking the add icon in the action ribbon
		When clicking the plus icon in the action ribbon
		Then a new quiz is added to the quiz list