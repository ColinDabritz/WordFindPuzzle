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
			//if(c >= 'A' && c <= 'Z')
			//{
			//	// make it lower case
			//	c = char.ToLower(c);
			//}
			// if letter is invalid
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
			//if(c >= 'A' && c <= 'Z')
			// { c = char.ToLower(c);} else

			// -- Under New Scheeme, NO INVALID LETTERS ARE ADDED, removed for speed --
			// if letter is invalid
				//	if(c < 'a' || c > 'z')
				//	{
				//		return null;
				//	}

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
        
		// The branches of this tree node, based on the 26 letter english alphabet
		CharTree[] children = new CharTree[26]; // default null
	}
}