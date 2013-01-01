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
        }
        
		// The branches of this tree node, based on the 26 letter english alphabet
		CharTree[] children = new CharTree[26]; // default null
	}
}