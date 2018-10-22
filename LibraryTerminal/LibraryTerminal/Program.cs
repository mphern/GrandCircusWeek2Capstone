using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;


namespace LibraryTerminal
{
    class Program
    {
        static void Main()
        {
            List<string> titles = new List<string>() {"To Kill a Mockingbird", "Frankenstein", "One Flew Over the Cuckoo's Nest", 
            "Lord of the Flies", "The Odyssey", "Scarlet Letter", "Call of the Wild", "A Clockwork Orange", "Catcher in the Rye", 
            "Tale of Two Cities", "The Adventures of Tom Sawyer", "Journey to the Center of the Earth"};

            List<string> authors = new List<string>() {"Lee, Harper", "Shelley, Mary", "Kesey, Ken", "Golding, William", "Homer",
            "Hawthorne, Nathaniel", "London, Jack", "Burgess, Anthony", "Salinger, J.D.", "Dickens, Charles", "Twain, Mark", "Verne, Jules"};

            List<string> bookAvailable = new List<string>() {"Y", "Y", "Y", "Y", "Y", "Y", "Y", "Y", "Y", "Y", "Y", "Y"};

            List<string> bookIdNumber = new List<string>() {"HL1", "MS1", "KK1", "WG1", "H01", "NH1", "JL1", "AB1", "JS1", "CD1", "MT1", "JV1"};

            Console.WriteLine("Welcome to the Grand Circus Library Catalog!");

            while(true)
            {
                DisplayMenu();
                int choice = GetChoice();
                if (choice == 1)
                    DisplayCatalog(ref titles, ref authors, ref bookAvailable, ref bookIdNumber);

                else if (choice == 2)
                    SearchByAuthor(ref titles, ref authors, ref bookAvailable, ref bookIdNumber);

                else if (choice == 3)
                    SearchByTitle(ref titles, ref authors, ref bookAvailable, ref bookIdNumber);

                else if (choice == 4)
                    CheckOut("\nEnter Book ID Number of book you are checking out: ", ref titles, ref bookAvailable, ref bookIdNumber);

                else if (choice == 5)
                    ReturnBook("\nEnter Book ID Number of book you are returning: ", ref titles, ref bookAvailable, ref bookIdNumber);

                else if (choice == 6)
                    AddBook(ref titles, ref authors, ref bookAvailable, ref bookIdNumber);

                else if (choice == 7)
                    break;
             
            }
            Console.WriteLine("\nGoodbye!");
            Console.ReadKey();
            
        }
        static void DisplayMenu()
        {
             
            Console.WriteLine("\nWhat would you like to do? (Enter 1-7)");
            Console.WriteLine("--------------------------------------");
            Console.WriteLine("1. See book catalog");
            Console.WriteLine("2. Search for book by author");
            Console.WriteLine("3. Search for book by title");
            Console.WriteLine("4. Check out a book");
            Console.WriteLine("5. Return a book");
            Console.WriteLine("6. Add a book to the Catalog");
            Console.WriteLine("7. Quit");
        }

        static int GetChoice()
        {
            int choice;
            Console.Write("> ");
            while(!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
            {
                Console.WriteLine("Invalid choice.  Try again.  Enter 1-7");
                Console.WriteLine("> ");
            }
            return choice;
        }

        static void DisplayCatalog(ref List<string> titles, ref List<string> authors, ref List<string> checkedOut, ref List<string> bookIdNumber)
        {
            string choice = ValidateChoice("Would you like the catalog ordered by Title or by Author? (Enter \"A\" for Author or \"T\" for Title): ", "A", "T");
            switch (choice)
            {
                case "A":
                    AlphabetizeAuthor(ref titles, ref authors, ref checkedOut, ref bookIdNumber);
                    break;

                case "T":
                    AlphabetizeTitle(ref titles, ref authors, ref checkedOut, ref bookIdNumber);
                    break;
            }

        }

        static string ValidateChoice(string message, string choice1, string choice2)
        {
            Console.Write(message);
            string choice = Console.ReadLine().ToUpper().Trim();
            while(choice != choice1 && choice != choice2)
            {
                Console.WriteLine("Invalid Input.");
                Console.WriteLine(message);
                choice = Console.ReadLine().ToUpper().Trim();
            }
            return choice;
        }

        static void AlphabetizeAuthor(ref List<string> titles, ref List<string> authors, ref List<string> checkedOut, ref List<string> bookIdNumber)
        {
            Console.WriteLine(string.Format("\n{0,-40} {1,-25} {2,-28} {3,0}", "Title", "Author", "Available?", "Book ID Number"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            List<string> authorsCopy = new List<string>(authors);
            List<string> authorsSeen = new List<string>();
            List<string> titlesCopy = new List<string>();
            List<string> checkedOutCopy = new List<string>();
            List<string> bookIdNumberCopy = new List<string>();
            authorsCopy.Sort();
            for (int i = 0; i < authorsCopy.Count; i++)
            {
                authorsSeen.Add(authorsCopy[i]);
                int index = FindNthOccuranceIndex(authorsCopy[i], authors, CountCommonStrings(authorsCopy[i], authorsSeen));  //Finds the nth occurance of the author index in order to find assign to corresponding info from the other lists.
                titlesCopy.Add(titles[index]);
                checkedOutCopy.Add(checkedOut[index]);
                bookIdNumberCopy.Add(bookIdNumber[index]);
            }

            for (int i = 0; i < authors.Count; i++)
            {
                Console.WriteLine(string.Format("{0,-40} {1,-25} {2,-28} {3,0}", titlesCopy[i], authorsCopy[i], checkedOutCopy[i], bookIdNumberCopy[i]));
            }
           
        }

        static void AlphabetizeTitle(ref List<string> titles, ref List<string> authors, ref List<string> checkedOut, ref List<string> bookIdNumber)
        {
            Console.WriteLine(string.Format("\n{0,-40} {1,-25} {2,-28} {3,0}", "Title", "Author", "Available?", "Book ID Number"));
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------");
            List<string> titlesCopy = new List<string>(titles);
            List<string> titlesSeen = new List<string>();
            List<string> authorsCopy = new List<string>();
            List<string> checkedOutCopy = new List<string>();
            List<string> bookIdNumberCopy = new List<string>();
            titlesCopy.Sort();
            for (int i = 0; i < titlesCopy.Count; i++)
            {
                titlesSeen.Add(titlesCopy[i]);
                int index = FindNthOccuranceIndex(titlesCopy[i], titles, CountCommonStrings(titlesCopy[i], titlesSeen));
                authorsCopy.Add(authors[index]);
                checkedOutCopy.Add(checkedOut[index]);
                bookIdNumberCopy.Add(bookIdNumber[index]);   
            }
            for (int i = 0; i < titlesCopy.Count; i++)
            {
                Console.WriteLine(string.Format("{0,-40} {1,-25} {2,-28} {3,0}", titlesCopy[i], authorsCopy[i], checkedOutCopy[i], bookIdNumberCopy[i]));
            }

        }

        static void SearchByAuthor(ref List<string> titles, ref List<string> authors, ref List<string> checkedOut, ref List<string> bookIdNumber)
        {
            string searchAgain = "YES";
            while (searchAgain == "YES" || searchAgain == "Y")
            {
                int authorsFound = 0;
                List<string> filteredTitles = new List<string>();
                List<string> filteredAuthors = new List<string>();
                List<string> filteredAvailable = new List<string>();
                List<string> filteredBookIdNumber = new List<string>();

                Console.Write("\nEnter Author Name (Last, First): ");
                string authorSearch = Console.ReadLine();
                foreach (string author in authors)
                {
                    if (Regex.IsMatch(author, authorSearch, RegexOptions.IgnoreCase))
                    {
                        authorsFound += 1;
                    }
                }
                if (authorsFound == 0)
                {
                    Console.WriteLine("No authors found");
                    searchAgain = GoAgain("\nWould you like to search the catalog again by author? (Y/N): ");
                }
                else
                {
                    List<string> authorsSeen = new List<string>();
                    foreach (string author in authors)
                    {
                        if (Regex.IsMatch(author, authorSearch, RegexOptions.IgnoreCase))
                        {
                            authorsSeen.Add(author);
                            int index = FindNthOccuranceIndex(author, authors, CountCommonStrings(author, authorsSeen));
                            filteredAuthors.Add(author);
                            filteredTitles.Add(titles[index]);
                            filteredAvailable.Add(checkedOut[index]);
                            filteredBookIdNumber.Add(bookIdNumber[index]);
                        }
                    }
                    AlphabetizeAuthor(ref filteredTitles, ref filteredAuthors, ref filteredAvailable, ref filteredBookIdNumber);
                    searchAgain = GoAgain("\nWould you like to search the catalog again by author? (Y/N): ");
                }
            }
        }

        static void SearchByTitle(ref List<string> titles, ref List<string> authors, ref List<string> checkedOut, ref List<string> bookIdNumber)
        {
            string searchAgain = "YES";
            while (searchAgain == "YES" || searchAgain == "Y")
            {
                int titlesFound = 0;
                List<string> filteredTitles = new List<string>();
                List<string> filteredAuthors = new List<string>();
                List<string> filteredAvailable = new List<string>();
                List<string> filteredBookIdNumber = new List<string>();

                Console.Write("\nEnter Title Name or Keyword: ");
                string titleSearch = Console.ReadLine();
                foreach (string title in titles)
                {
                    if (Regex.IsMatch(title, titleSearch, RegexOptions.IgnoreCase))
                    {
                        titlesFound += 1;
                    }
                }
                if (titlesFound == 0)
                {
                    Console.WriteLine("No titles found");
                    searchAgain = GoAgain("\nWould you like to search the catalog again by title? (Y/N): ");
                }
                else
                {
                    List<string> titlesSeen = new List<string>();
                    foreach (string title in titles)
                    {
                        if (Regex.IsMatch(title, titleSearch, RegexOptions.IgnoreCase))
                        {
                            titlesSeen.Add(title);
                            int index = FindNthOccuranceIndex(title, titles, CountCommonStrings(title, titlesSeen));
                            filteredTitles.Add(title);
                            filteredAuthors.Add(authors[index]);
                            filteredAvailable.Add(checkedOut[index]);
                            filteredBookIdNumber.Add(bookIdNumber[index]);
                        }
                    }
                    AlphabetizeTitle(ref filteredTitles, ref filteredAuthors, ref filteredAvailable, ref filteredBookIdNumber);
                    searchAgain = GoAgain("\nWould you like to search the catalog again by title? (Y/N): ");
                }
            }
        }

        static string GoAgain(string message)
        {
            Console.Write(message);
            string searchAgain = Console.ReadLine().ToUpper().Trim();
            while(searchAgain != "YES" && searchAgain != "Y" && searchAgain != "NO" && searchAgain != "N")
            {
                Console.WriteLine("Invalid response. "+ message);
                searchAgain = Console.ReadLine().ToUpper().Trim();
            }
            return searchAgain;
        }

        static void CheckOut(string message, ref List<string> titles, ref List<string> bookAvailable, ref List<string> bookIdNumber)
        {
            string goAgain = "YES";
            while (goAgain == "YES" || goAgain == "Y")
            {
                Console.Write(message);
                int booksFound = 0;
                string bookID = Console.ReadLine().ToUpper().Trim();
                for (int i = 0; i < bookIdNumber.Count; i++)
                {
                    if (bookID == bookIdNumber[i])
                    {
                        booksFound += 1;
                        if (bookAvailable[i] == "N")
                        {
                            Console.WriteLine("Book is already checked out");
                            goAgain = GoAgain("Would you like to check out a different book? (Y/N): ");
                            
                        }
                        else
                        {
                            DateTime dueDate = DateTime.Now.AddDays(14);
                            bookAvailable[i] = "N (Due Back "+dueDate.ToString("MM/dd/yyyy")+")";
                            Console.WriteLine("\n"+ titles[i] + " has been checked out.  Book is due back "+dueDate.ToString("MM/dd/yyyy")+".\n");
                            goAgain = GoAgain("Would you like to check out another book? (Y/N): ");
                            
                        }
                    }
                }
                if(booksFound == 0)
                {
                    Console.WriteLine("Book ID Number not found.");
                    goAgain = GoAgain("Would you like to search for a different Book ID Number? (Y/N): ");
                }

            }
        }

        static void ReturnBook(string message, ref List<string> titles, ref List<string> bookAvailable, ref List<string> bookIdNumber)
        {
            string goAgain = "YES";
            while (goAgain == "YES" || goAgain == "Y")
            {
                Console.Write(message);
                int booksFound = 0;
                string bookID = Console.ReadLine().ToUpper().Trim();
                for (int i = 0; i < bookIdNumber.Count; i++)
                {
                    if (bookID == bookIdNumber[i])
                    {
                        booksFound += 1;
                        if (bookAvailable[i][0] == 'N')
                        {
                            bookAvailable[i] = "Y";
                            Console.WriteLine("\n" + titles[i] + " has been returned.\n");
                            goAgain = GoAgain("Would you like to return another book? (Y/N): ");
                        }
                        else
                        {
                            Console.WriteLine("\nThis book is already checked in.\n");
                            goAgain = GoAgain("Would you like to return a different book? (Y/N): ");
                        }
                    }
                }
                if (booksFound == 0)
                {
                    Console.WriteLine("Book ID Number not found.");
                    goAgain = GoAgain("Would you like to search for a different Book ID Number? (Y/N): ");
                }
            }
        }

        static void AddBook(ref List<string> titles, ref List<string> authors, ref List<string> checkOut, ref List<string> bookIdNumber)
        {
            
            {
                Console.Write("Enter name of book: ");
                titles.Add(Console.ReadLine().Trim());
                Console.Write("Enter first name of author: ");
                string firstName = Console.ReadLine().Trim();
                Console.Write("Enter last name of author: ");
                string lastName = Console.ReadLine().Trim();
                if (lastName != "")
                {
                    authors.Add(lastName + ", " + firstName);
                    checkOut.Add("Y");
                    string initials = (firstName[0].ToString() + lastName[0].ToString()).ToUpper();
                    bookIdNumber.Add(GenerateBookNumber(initials, ref bookIdNumber));
                }
                else
                {
                    authors.Add(firstName);
                    checkOut.Add("Y");
                    string initials = firstName[0].ToString().ToUpper()+"0";
                    bookIdNumber.Add(GenerateBookNumber(initials, ref bookIdNumber));
                }
                Console.WriteLine("Book has been added");
            }

        }

        static string GenerateBookNumber(string initials, ref List<string> bookIdNumber)
        {
            int matches = 0;
            foreach(string bookID in bookIdNumber)
            {
                if (Regex.IsMatch(bookID, initials))
                matches += 1;
            }

            return initials + (matches + 1).ToString();
        }

        static int FindNthOccuranceIndex(string str, List<string> list, int n)
        {
            int count = 0;
            int index = 0;
            for(int i = 0; count < n; i++)
            {
                if (str == list[i])
                    count += 1;
                if (count == n)
                    index = i;
            }
            return index;
        }  //Method that returns the index of the nth occurance of a string in case there is multiple books by the same author.

        static int CountCommonStrings(string str, List<string> list)
        {
            int count = 0;
            foreach(string String in list)
            {
                if(str == String)
                    count += 1;
            }
            return count;
        }   //Method that counts the number of occurances of the same string in a list.
    }
}
