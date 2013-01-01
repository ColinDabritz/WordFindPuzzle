using System;
using System.Collections;

namespace WordFindPuzzle
{
	// The CharTree structure is a 26 character alphabet based tree
	// each 'level' has pointers (default null) to a branch CharTree.
	public class CharTree
	{
		public CharTree()
		{		
			// default constructor is fine
		}
		// simply returns the child (which could be null)
		// or null if the character is bad
		public CharTree getChild(char c)
		{
			// -- if capital letters count on search: --
			// if it is a capital letter (matters on search)
			/*
			if(c >= 'A' && c <= 'Z')
			{
				// make it lower case
				c = char.ToLower(c);
			}
			*/
			// else
			if(c < 'a' || c > 'z')
			{
				return null;
			}

			return children[c-'a'];
		}

		// returns the existing branch or a new branch
		// based on a given input
		// returns null if given a bad character
		public CharTree Add(char c)
		{
			// -- if capital letters count on creation: --
			// NOTE: Puzzle is all lower case,
			// and we create the puzzleTree from the puzzle
			// so this is not needed
			// if it is a capital letter (matters on search)
			/*
			if(c >= 'A' && c <= 'Z')
			{
				// make it lower case
				c = char.ToLower(c);
			}
			*/
			// else
			// -- Under New Scheeme, NO INVALID LETTERS ARE ADDED, removed for speed --
			//  if letter is invalid
			/*
			if(c < 'a' || c > 'z')
			{
				return null;
			}
			*/

			int index = c-'a';

			if(children[index] == null)
			{
				return (children[index] = new CharTree());
			}
			else
			{
				return children[index];
			}
		}

		// Assumes valid input
		public void AddBranch(string word)
		{
			CharTree currentBranch  = this;
			foreach(char c in word)
			{
				currentBranch = currentBranch.Add(c);
			}
		
			/* // Easy recursive version, slower
			CharTree currentBranch = this;

			for(int charIndex = 0; charIndex < word.Length; charIndex++)
			{
				currentBranch = currentBranch.Add(word[charIndex]);
			}
			*/
		}

		public bool HasBranch(string word)
		{
            
			// forward and reverse code
			CharTree forwardBranch = this;
			CharTree reverseBranch = this;
			int index;
			for(	index = 0;
					index < word.Length &&
							((forwardBranch != null)||( reverseBranch != null));
					index++
						)
			{
				if(forwardBranch != null)
				{
					forwardBranch = forwardBranch.getChild(word[index]);
				}
				if(reverseBranch != null)
				{
					reverseBranch = reverseBranch.getChild(word[word.Length - 1 - index]);
				}
			}
			if(index == word.Length // made it to end of word
               && (forwardBranch != null // has an active branch
                   || reverseBranch != null))
			{
				return true;
			}
			return false;

            /*
            // forward only code
            CharTree childBranch = this;

            foreach(char c in word)
            {
                childBranch = childBranch.getChild(c);
                if(childBranch == null)
                {
                    return false;
                }
            }
            return true;
            */
        }
        
		// The branches of this tree node, based on the 26 letter english alphabet
		CharTree[] children = new CharTree[26]; // default null
	}
}