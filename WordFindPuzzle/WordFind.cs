using System;
using System.IO;    // For StreamReader
using System.Text;  // For StringBuilder

namespace WordFindPuzzle
{
	class WordFindPuzzle
	{
		
		static void Main(string[] args)  // arg[0] is dictionary, arg[1] is puzzle
		{
			#if (DEBUG)
			// -- Hardwired command line arguments --
			// args = "english-words.10 puzzle0.txt".Split(' ');
			// args = "english-words.10 puzzle1.txt".Split(' ');
			args = "english-words.95 puzzle4.txt".Split(' ');
			#endif			

			WordFinder wordFinder = new WordFinder();

			wordFinder.ReadPuzzle(args[1]);

			wordFinder.GenerateFullPuzzle();

			wordFinder.BuildPuzzleTree();

			wordFinder.ProcessDictionary(args[0]);

		}
	}
}
