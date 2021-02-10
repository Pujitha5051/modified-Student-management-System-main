using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace modified_Student_management_System
{
    public class optionCheckException : ApplicationException
    {
        public override String Message
        {
            get
            {
                return "Please select a valid option from the menu.";
            }
        }
    }
    public class emptyFieldException : ApplicationException
    {
        public override String Message
        {
            get
            {
                return "Field can't be left blank.";
            }
        }
    }
    public class rollNumberCheckException : ApplicationException
    {
        public override String Message
        {
            get
            {
                return "Roll number can be a numeric value only";
            }
        }
    }

    public class studentNameCheckException : ApplicationException
    {
        public override String Message
        {
            get
            {
                return "Student's name can only contain alphabets";
            }
        }
    }
    public class studentMarksCheckException : ApplicationException
    {
        public override string Message
        {
            get
            {
                return "Marks can be a number only.";
            }
        }
    }
    public class schoolNameCheckException : ApplicationException
    {
        public override String Message
        {
            get
            {
                return "School name can contain only alphabets";
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            School schoolObject = new School();

            void onStart()
            {              
                Console.WriteLine("Enter School Name :");
                schoolObject.SchoolName = Console.ReadLine();

                // School name validation code.
                try
                {
                    if (String.IsNullOrEmpty(schoolObject.SchoolName))
                    {
                        throw new emptyFieldException();

                    }
                    else if (schoolObject.SchoolName.Any(char.IsDigit))
                    {
                        throw new schoolNameCheckException();
                    }
                    else if (schoolObject.SchoolName.Length < 4)
                    {
                        Console.WriteLine("School name can't have less than 4 characters.");

                        //goto onStart method.
                        onStart();
                    }
                }
                catch (emptyFieldException msg)
                {
                    Console.WriteLine(msg.Message);

                    //goto onStart method.
                    onStart();
                }
                catch (schoolNameCheckException msg)
                {
                    Console.WriteLine(msg.Message);

                    //goto onStart method.
                    onStart();
                }
            }           
 
        //trigger start function.
            onStart();
            

      // School name along with the menu will be displayed when below code will run.

        void menu() { 
            Console.WriteLine($"\nWelcome to {schoolObject.SchoolName} Student information management\n");
            Console.WriteLine("1. Add Student.");
            Console.WriteLine("2. Add marks for student.");
            Console.WriteLine("3. Show student's progress card.\n");
            Console.WriteLine("Please provide valid input from menu options :");


            string optionString =Console.ReadLine();

            //Regex to check if a string contains digit or not.

            Regex r = new Regex("^[0-9]+$");
            try
            {
                if (String.IsNullOrEmpty(optionString))
                {
                    throw new emptyFieldException();
                }
                if (!r.IsMatch(optionString))
                {
                    Console.Clear();                  
                    Console.WriteLine("Invalid option type");

                        //goto menu method.
                        menu();
                    }
            }
            catch (emptyFieldException msg)
            {
                Console.Clear();
                Console.WriteLine(msg.Message);

                    //goto menu method.
                    menu();
                }          

            int option = Convert.ToInt32(optionString);

            try
            {           
                if (option  != 1 && option != 2 && option != 3)
                {
                    throw new optionCheckException();
                }
            }
            catch (optionCheckException msg)
            {
                Console.Clear();
                Console.WriteLine(msg.Message);

                    //goto menu method.
                    menu();
            }

                // If option 1 is selected from the menu following code fill run.

                if (option == 1)
                {
                    Student studentObj = new Student();

                ADD_STUDENT_ROLL:
                    try
                    {
                        Console.WriteLine("Enter Student's Roll Number ");

                        studentObj.StudentRollNumberString = Console.ReadLine();

                        // check roll number field is empty or not.

                        if (String.IsNullOrEmpty(studentObj.StudentRollNumberString))
                        {
                            throw new emptyFieldException();
                        }
                        // check roll number field contains only numbers.

                        else if (!r.IsMatch(studentObj.StudentRollNumberString))
                        {
                            throw new rollNumberCheckException();
                        }
                        studentObj.StudentRollNumber = Convert.ToInt32(studentObj.StudentRollNumberString);
                    }
                    catch (emptyFieldException msg)
                    {
                        Console.Clear();
                        Console.WriteLine(msg.Message);

                        goto ADD_STUDENT_ROLL;
                    }
                    catch (rollNumberCheckException msg)
                    {
                        Console.Clear();
                        Console.WriteLine(msg.Message);

                        goto ADD_STUDENT_ROLL;
                    }

                ADD_STUDENT_NAME:
                    try
                    {
                        Console.WriteLine("Enter Student's Name");
                        studentObj.StudentName = Console.ReadLine();

                        //declare a regex for alphabetic pattern.
                        Regex ra = new Regex("^[a-zA-Z ]+$");

                        // check Student Name field is empty or not.

                        if (String.IsNullOrEmpty(studentObj.StudentName))
                        {
                            new emptyFieldException();
                        }
                        // check Student Name field has only alphabets or not.

                        else if (!ra.IsMatch(studentObj.StudentName))
                        {
                            throw new studentNameCheckException();
                        }
                        else if (studentObj.StudentName.Length < 4)
                        {
                            Console.WriteLine("Student's name should have atleast 4 characters .");
                            goto ADD_STUDENT_NAME;
                        }

                    }
                    catch (emptyFieldException msg)
                    {
                        Console.Clear();
                        Console.WriteLine(msg.Message);
                        goto ADD_STUDENT_NAME;
                    }
                    catch (studentNameCheckException msg)
                    {
                        Console.Clear();
                        Console.WriteLine(msg.Message);
                        goto ADD_STUDENT_NAME;
                    }

                    schoolObject.Student.Add(studentObj);

                    //goto menu method.
                    menu();

                }

                // If option 2 is selected from the menu following code fill run.

                else if (option == 2)
                {
                    string rollNumber; //making a rollNumber variable to perform operation for that student.

                    void validateRollNumber()
                    {   //function to verify roll number .

                        Console.WriteLine("Enter Student's roll number :");
                        rollNumber = Console.ReadLine();

                        bool Flag = false;

                        schoolObject.Student.ForEach(roll =>
                        {                           
                            try
                            {
                                // declare a regex for digit pattern.

                                Regex r = new Regex("^[0-9]+$");

                                //if roll number entered matches with the stored roll number then flag is set to true and returned.
                                if ((roll.StudentRollNumberString == rollNumber))
                                {
                                    Flag = true;
                                    return;
                                }
                                else if (String.IsNullOrEmpty(rollNumber))
                                {
                                    throw new emptyFieldException();
                                }
                                //check if roll number matches the regex pattern
                                else if (!r.IsMatch(rollNumber))
                                {
                                    throw new rollNumberCheckException();
                                }

                            }
                            catch (emptyFieldException msg)
                            {
                                Console.WriteLine(msg.Message);
                                validateRollNumber();
                            }
                            catch (rollNumberCheckException msg)
                            {
                                Console.WriteLine(msg.Message);
                                validateRollNumber();
                            }
                        });

                        if (!Flag)
                        {
                            Console.WriteLine("No such student roll number exists");
                            validateRollNumber();
                        }
                    }
                    // calling validateRollNumber method to perform roll number validation
                    validateRollNumber();

                    //create progress card object
                    ProgressCard progressCardObject = new ProgressCard();

                TELUGU_MARKS:
                    try
                    {
                        Console.WriteLine("Enter marks scored in Telugu :");
                        var teluguMarks = Console.ReadLine();


                        if (String.IsNullOrEmpty(teluguMarks))
                        {
                            throw new emptyFieldException();
                        }
                        else if (!r.IsMatch(teluguMarks))
                        {
                            throw new studentMarksCheckException();
                        }

                        var teluguMarksInt = Convert.ToInt32(teluguMarks);

                        //check if marks range is between 0 & 100.
                        if (teluguMarksInt > 100 || teluguMarksInt < 0)
                        {
                            Console.WriteLine("Marks should range between 0 & 100 .");
                            goto TELUGU_MARKS;
                        }
                        progressCardObject.TeluguMarks = teluguMarksInt;
                    }
                    catch (emptyFieldException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto TELUGU_MARKS;
                    }
                    catch (studentMarksCheckException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto TELUGU_MARKS;
                    }

                HINDI_MARKS:

                    try
                    {
                        Console.WriteLine("Enter marks scored in Hindi :");
                        var hindiMarksString = Console.ReadLine();

                        // check Marks field is empty or not.

                        if (String.IsNullOrEmpty(hindiMarksString))
                        {
                            throw new emptyFieldException();
                        }
                        else if (!r.IsMatch(hindiMarksString))
                        {
                            throw new studentMarksCheckException();
                        }
                        var hindiMarksInt = Convert.ToInt32(hindiMarksString);

                        //check if marks range is between 0 & 100.

                        if (hindiMarksInt > 100 || hindiMarksInt < 0)
                        {
                            Console.WriteLine("Marks should range between 0 & 100 .");
                            goto HINDI_MARKS;

                        }
                        progressCardObject.HindiMarks = hindiMarksInt;
                    }

                    catch (emptyFieldException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto HINDI_MARKS;

                    }
                    catch (studentMarksCheckException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto HINDI_MARKS;

                    }

                ENGLISH_MARKS:

                    try
                    {
                        Console.WriteLine("Enter marks scored in English :");
                        var englishMarksString = Console.ReadLine();

                        // check Marks field is empty or not.

                        if (String.IsNullOrEmpty(englishMarksString))
                        {
                            throw new emptyFieldException();
                        }
                        else if (!r.IsMatch(englishMarksString))
                        {
                            throw new studentMarksCheckException();
                        }
                        var englishMarksInt = Convert.ToInt32(englishMarksString);

                        //check if marks range is between 0 & 100.

                        if (englishMarksInt < 0 || englishMarksInt > 100)
                        {
                            Console.WriteLine("Marks should range between 0 & 100 .");
                            goto ENGLISH_MARKS;
                        }
                        progressCardObject.EnglishMarks = englishMarksInt;
                    }
                    catch (emptyFieldException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto ENGLISH_MARKS;
                    }
                    catch (studentMarksCheckException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto ENGLISH_MARKS;
                    }

                MATHS_MARKS:
                    try
                    {
                        Console.WriteLine("Enter marks scored in Maths :");
                        var mathsMarksString = Console.ReadLine();

                        // check Marks field is empty or not.

                        if (String.IsNullOrEmpty(mathsMarksString))
                        {
                            throw new emptyFieldException();

                        }
                        else if (!r.IsMatch(mathsMarksString))
                        {
                            throw new studentMarksCheckException();
                        }
                        var mathsMarksInt = Convert.ToInt32(mathsMarksString);

                        //check if marks range is between 0 & 100.

                        if (mathsMarksInt < 0 || mathsMarksInt > 100)
                        {
                            Console.WriteLine("Marks should range between 0 & 100 .");
                            goto MATHS_MARKS;
                        }
                        progressCardObject.MathsMarks = mathsMarksInt;

                    }
                    catch (emptyFieldException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto MATHS_MARKS;
                    }
                    catch (studentMarksCheckException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto MATHS_MARKS;
                    }

                SCIENCE_MARKS:

                    try
                    {
                        Console.WriteLine("Enter marks scored in Science :");
                        var scienceMarksString = Console.ReadLine();

                        // check Marks field is empty or not.

                        if (String.IsNullOrEmpty(scienceMarksString))
                        {
                            throw new emptyFieldException();

                        }
                        else if (!r.IsMatch(scienceMarksString))
                        {
                            throw new studentMarksCheckException();
                        }

                        var scienceMarksInt = Convert.ToInt32(scienceMarksString);

                        //check if marks range is between 0 & 100.
                        if (scienceMarksInt < 0 || scienceMarksInt > 100)
                        {
                            Console.WriteLine("Marks should range between 0 & 100 .");
                            goto SCIENCE_MARKS;
                        }

                        progressCardObject.ScienceMarks = scienceMarksInt;
                    }
                    catch (emptyFieldException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto SCIENCE_MARKS;
                    }
                    catch (studentMarksCheckException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto SCIENCE_MARKS;
                    }

                SOCIAL_MARKS:
                    try
                    {
                        Console.WriteLine("Enter marks scored in Social :");
                        var socialMarksString = Console.ReadLine();

                        // check Marks field is empty or not.

                        if (String.IsNullOrEmpty(socialMarksString))
                        {
                            throw new emptyFieldException();

                        }
                        else if (!r.IsMatch(socialMarksString))
                        {
                            throw new studentMarksCheckException();
                        }
                        //check if marks range is between 0 & 100.

                        var socialMarksInt = Convert.ToInt32(socialMarksString);

                        if (socialMarksInt < 0 || socialMarksInt > 100)
                        {
                            Console.WriteLine("Marks should range between 0 & 100 .");
                            goto SOCIAL_MARKS;
                        }
                    }
                    catch (emptyFieldException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto SOCIAL_MARKS;
                    }
                    catch (studentMarksCheckException msg)
                    {
                        Console.WriteLine(msg.Message);
                        goto SOCIAL_MARKS;
                    }
                    Console.Clear();

                    //Adding all marks and pushing into total marks properties of the ProgressCard.

                    progressCardObject.Totalmarks = (progressCardObject.TeluguMarks + progressCardObject.HindiMarks + progressCardObject.EnglishMarks +
                                                    progressCardObject.MathsMarks + progressCardObject.ScienceMarks + progressCardObject.SocialMarks);

                    progressCardObject.Percentage = (double)(progressCardObject.Totalmarks) / 6;

                    //iterate through all the objects in Student prop. and match with the roll number.
                    schoolObject.Student.ForEach(roll =>
                    {
                        if (String.Equals(roll.StudentRollNumberString, rollNumber))
                        {
                            roll.ProgressCard.Add(progressCardObject);
                        }

                    });

                    //goto menu method.
                    menu();
                }

                // If option 3 is selected from the menu following code fill run.

                else if (option == 3)
                {
                    string rollNumber; //making a rollNumber variable to perform operation for that student.

                    void validateRollProgressCard()
                    {   //function to verify roll number .

                        Console.WriteLine("Enter Student's roll number :");
                        rollNumber = Console.ReadLine();

                        bool Flag = false;

                        try
                        {
                            if (String.IsNullOrEmpty(rollNumber))
                            {
                                throw new emptyFieldException();
                            }
                            else if (!r.IsMatch(rollNumber))
                            {
                                throw new rollNumberCheckException();
                            }

                            schoolObject.Student.ForEach(roll =>
                            {
                                Console.WriteLine(roll.StudentRollNumberString);

                                Regex r = new Regex("^[0-9]+$");
                                if (String.Equals(roll.StudentRollNumberString, rollNumber))
                                {
                                    Flag = true;
                                    Console.WriteLine("Student's Roll Number : " + roll.StudentRollNumberString);
                                    Console.WriteLine("Student's Name : " + roll.StudentName + "\n");
                                    Console.WriteLine("Student's Marks");

                                    roll.ProgressCard.ForEach(card =>
                                    {
                                    //display telugu marks.
                                    Console.WriteLine("Telugu :" + card.TeluguMarks);

                                    //display telugu marks.
                                    Console.WriteLine("Hindi :" + card.HindiMarks);

                                    //display english marks.
                                    Console.WriteLine("English :" + card.EnglishMarks);

                                    //display maths marks.
                                    Console.WriteLine("Maths :" + card.MathsMarks);

                                    //display Science marks.
                                    Console.WriteLine("Science :" + card.ScienceMarks);

                                    //display Social marks.
                                    Console.WriteLine("Social :" + card.SocialMarks + "\n");

                                    //display total marks.

                                    Console.WriteLine("Total Marks :" + card.Totalmarks);

                                    //display Percentage upto 2 decimal points.
                                    Console.WriteLine("Percentage :" + Math.Round((double)card.Totalmarks / 6, 2) + " %");

                                        menu();

                                    });
                                }
                                else if (!Flag)
                                {
                                    Console.WriteLine("No such student roll number exists");

                                //call the function to validate roll number again
                                validateRollProgressCard();
                                }
                            });
                        }
                        catch (emptyFieldException msg)
                        {
                            Console.WriteLine(msg.Message);
                            validateRollProgressCard();
                        }
                        catch (rollNumberCheckException msg)
                        {
                            Console.WriteLine(msg.Message);
                            validateRollProgressCard();
                        }
                    }
                    // calling validateRollNumber method to perform roll number validation
                    validateRollProgressCard();
                }
            }
            menu();
        }
    }
}
        


