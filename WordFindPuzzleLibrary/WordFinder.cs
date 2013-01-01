using System;
using System.IO;    // For StreamReader
using System.Text;  // For StringBuilder

namespace WordFindPuzzle
{
	public class WordFinder
	{
		public WordFinder()
		{
			// Default constructor is fine
		}

		public void ReadPuzzle(string fileName)
		{
			using (	StreamReader puzzleSource = new StreamReader(fileName))
			{
				puzzle = puzzleSource.ReadToEnd();
			}
		}

		public void GenerateFullPuzzle()
		{
			// setup string builder, for building other directions
			// capacity is roughly the puzzle size * 8 directions
			puzzleBuilder = new StringBuilder(puzzle, puzzle.Length * 8);

			// Make all gaps '\n' only
			puzzleBuilder.Replace("\r\n","\n");	
			puzzle = puzzleBuilder.ToString();

			AddDiagonals();
			AddVertical();

			// Shorten String by removeing extra space (very fast)
			puzzleBuilder.Replace("\n\n\n\n\n","\n");
			puzzleBuilder.Replace("\n\n\n\n","\n");
			puzzleBuilder.Replace("\n\n\n","\n");
			puzzleBuilder.Replace("\n\n","\n");	

			// add reversal of puzzle to puzzle
			puzzle = puzzleBuilder.ToString();			
			// for the whole puzzle, in reverse, add it to the puzzleBuilder
			/*
			for(int index = puzzle.Length - 1; index >= 0; index--)
			{
				puzzleBuilder.Append(puzzle[index]);
			}
			// tree setup expects puzzle to end with a divider
			puzzleBuilder.Append("\n");

			// Assign final puzzle
			puzzle = puzzleBuilder.ToString();
			*/

			// Puzzle string completely generated for all directions
		}

		public void BuildPuzzleTree()
		{
			// Initialize bew puzzleTree
			puzzleTree = new CharTree();

			// For all the characters in the full puzzle
			for(int charIndex = 0; charIndex < puzzle.Length; charIndex++)
			{
				// add the substring from here to the end of this puzzle section to the tree
				puzzleTree.AddBranch(
					puzzle.Substring(charIndex,puzzle.IndexOf('\n',charIndex)-charIndex)
					);		
			}
		}

		public void ProcessDictionary(string fileName)
		{
			using(StreamReader dictionarySource = new StreamReader(fileName))
			{
				string word;
				// for each word in dictionary (read a line at a time from the dictionary file)
				while((word = dictionarySource.ReadLine()) != null)
				{
					if(HasWordInPuzzle(word))
					{
						// Print it out (words are read alphabetically, and printed the same way)
						Console.WriteLine(word);
					}
				}
			}
		}

		public bool HasWordInPuzzle(string word)
		{
			// if the word is of a valid length (more than 2 letters)
			//   and it is in the tree
			if(word.Length > 2 && puzzleTree.HasBranch(word))
			{
				// we found a word in the puzzle
				return true;				
			}
			return false;
		}


		private void AddDiagonals()
		{
			AddDiagonalRight();
			AddDiagonalLeft();
		}

		private void AddDiagonalRight()
		{
			int width = puzzle.IndexOf("\n") + 1;

			// for every character on the first puzzle line
			for(int index = 0; index <= width && index < puzzle.Length; index++)
			{
				// grab the diagonal right component (with 'wrapping')
				// Similarly, this also grabs breaks at appropriate points
				for(int drindex = index; drindex < puzzle.Length; drindex += width+1)
				{
					puzzleBuilder.Append(puzzle[drindex]);
				}
				puzzleBuilder.Append('\n'); // Maintain gaps
			}
		}

		private void AddDiagonalLeft()
		{
			int width = puzzle.IndexOf("\n") + 1;

			// for every character on the first puzzle line
			for(int index = 0; index < width && index < puzzle.Length; index++)
			{		
				// grab the diagonal left component
				// Note, this includes wrapping, but grabs breaks ('\n') at
				//   appropriate points to separate the lines
				for(int dlindex = index;dlindex < puzzle.Length; dlindex += width-1)
				{
					puzzleBuilder.Append(puzzle[dlindex]);
				}
				puzzleBuilder.Append('\n'); // Maintain gaps
			}
		}

		private void AddVertical()
		{
			// Generate other directions
			int width = puzzle.IndexOf("\n") + 1;

			// for every character on the first puzzle line
			for(int index = 0; index < width && index < puzzle.Length; index++)
			{
				// grab the vertical component based on that char (vertical down)
				for(int vindex = index; vindex < puzzle.Length; vindex += width)
				{
					puzzleBuilder.Append(puzzle[vindex]);
				}
				puzzleBuilder.Append('\n'); // Maintain gaps, we don't want weird wrapping bugs
			}
		}
		private string puzzle = string.Empty;	
		public string Puzzle
		{
			get
			{
				return puzzle;
			}
		}

		private StringBuilder puzzleBuilder = null;
		private CharTree puzzleTree = null;
	}
}
