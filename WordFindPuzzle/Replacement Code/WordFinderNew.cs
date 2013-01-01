using System;
using System.IO;    // For StreamReader
using System.Text;  // For StringBuilder

namespace WordFindPuzzle
{
	class WordFinder
	{		
		public static string GenerateFullPuzzle(string puzzle);

		public static void Main(string[] args)  // arg[0] is dictionary, arg[1] is puzzle
		{
			#if (DEBUG)
			// -- Hardwired command line arguments --
			// args = "english-words.10 puzzle0.txt".Split(' ');
			// args = "english-words.10 puzzle1.txt".Split(' ');
			args = "english-words.95 puzzle4.txt".Split(' ');
			#endif

			// string puzzle = GenerateFullPuzzle(GetPuzzle(arg[1]));

			#region Read and Organize Puzzle
			string puzzle;
			using (	StreamReader puzzleSource = new StreamReader(args[1]))
			{
				// Read entire puzzle
				puzzle = puzzleSource.ReadToEnd();
			}

			// setup string builder, for building other directions
			// capacity is roughly the puzzle size * 8 directions
			StringBuilder puzzleBuilder = new StringBuilder(puzzle, puzzle.Length * 8);

			// Make all gaps '\n' only
			puzzleBuilder.Replace("\r\n","\n");	
			puzzle = puzzleBuilder.ToString();

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

				// grab the diagonal left component
				// Note, this includes wrapping, but grabs breaks ('\n') at
				//   appropriate points to separate the lines
				for(int dlindex = index;dlindex < puzzle.Length; dlindex += width-1)
				{
					puzzleBuilder.Append(puzzle[dlindex]);
				}
				puzzleBuilder.Append('\n'); // Maintain gaps

				// grab the diagonal right component (with 'wrapping')
				// Similarly, this also grabs breaks at appropriate points
				for(int drindex = index; drindex < puzzle.Length; drindex += width+1)
				{
					puzzleBuilder.Append(puzzle[drindex]);
				}
				puzzleBuilder.Append('\n'); // Maintain gaps
			}
			// one final diagonal right component, because of odd 'off by one' behavior
			for(int drindex = width; drindex < puzzle.Length; drindex += width+1)
			{
				puzzleBuilder.Append(puzzle[drindex]);
			}
			
			
			// Puzzle Generated Forwards for all directions

			// Shorten String by removeing extra space (very fast)
			puzzleBuilder.Replace("\n\n\n\n\n","\n");
			puzzleBuilder.Replace("\n\n\n\n","\n");
			puzzleBuilder.Replace("\n\n\n","\n");
			puzzleBuilder.Replace("\n\n","\n");	

			// add reversal of puzzle to puzzle
			puzzle = puzzleBuilder.ToString();			
			// for the whole puzzle, in reverse, add it to the puzzleBuilder
			for(int index = puzzle.Length - 1; index >= 0; index--)
			{
				puzzleBuilder.Append(puzzle[index]);
            }
			// tree setup expects puzzle to end with a divider
			puzzleBuilder.Append("\n");

			// Assign final puzzle
			puzzle = puzzleBuilder.ToString();

			// Puzzle string completely generated for all directions

			#endregion

			// CharTree puzzleTree = MakePuzzleTree(string fullPuzzle);

			#region Set up puzzleTree
			
			// CharTree is a character based tree structure

			// Initializing puzzleTree and setting up 'branch' reference (pointer)
			CharTree puzzleTree = new CharTree();
			CharTree currentBranch;
			
			// loops use this index, and sometimes it is needed after the loop
			int charIndex;

			// For all the characters in the full puzzle
			for(charIndex = 0; charIndex < puzzle.Length; charIndex++)
			{
				// reset the branch node to the root
				currentBranch = puzzleTree;
				
				// add all the chars between the current one and the next '\n' to the tree (as a path)
				for(int addIndex = charIndex; addIndex < puzzle.IndexOf('\n',charIndex);addIndex++)
				{
					currentBranch = currentBranch.Add(puzzle[addIndex]);
				}				
			}

			// puzzleTree filled

			#endregion

			// PrintDictionary(string dictionaryFileName, CharTree puzzleTree);

			#region Check Dictionary Words

			
			// for each word in dictionary (read a line at a time from the dictionary file)
			using (StreamReader dictionarySource = new StreamReader(args[0]))
			{
				string word;
				while((word = dictionarySource.ReadLine()) != null)
				{
					// if the word is of a valid length (more than 2 letters)
					if(word.Length > 2) 
					{
						// reset tree branch to root node
						currentBranch = puzzleTree;										
						for(charIndex = 0;				// for each character in the word
							charIndex < word.Length		// while charIndex is inside the word
							&& currentBranch != null;	// and the branch exists
							charIndex++)
						{
							// walk down the tree based on the words characters
							currentBranch = currentBranch.getChild(word[charIndex]);
						}
						// if we found a word (we ended on a valid branch at the end of our word)
						if(charIndex == word.Length && currentBranch != null) 
						{
							// Print it out (words are read alphabetically, and printed the same way)
							Console.WriteLine(word);
						}
					}
				}
			}

			#endregion

			// Done
		}
	}
}
