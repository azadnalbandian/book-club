using BookClub.Code.Classes;
using System;
using System.Threading.Tasks;

namespace BookClub
{
    class Program
    {
        static async Task Main(string[] args)
        {
            int id = IDEntry();

            string book = await GetGoodReadsAsync(id);
            while (book == string.Empty)
            {
                Console.WriteLine("Looks like there was an issue. Please try again");
                IDEntry();
            }

            Console.WriteLine($"Your chosen book is { book }! Press any key to quit.");
            Console.Read();
        }

        static int IDEntry ()
        {
            Console.WriteLine("Please enter your Goodreads user ID.");
            int id;

            while (!int.TryParse(Console.ReadLine(), out id))
            {
                Console.Clear();
                Console.WriteLine("Invalid ID, please try again.");
            }

            return id;
        }

        static async Task<string> GetGoodReadsAsync(int id)
        {
            GoodreadsObject api = new GoodreadsObject(id);

            Console.WriteLine("Picking a book...");
            string result = await api.GetBookToRead();
            return result;
        }
    }
}
