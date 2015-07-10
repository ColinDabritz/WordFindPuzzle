using System;
using WordFindPuzzle;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WordFindPuzzleTests
{
	[TestClass]
	public class WordFinderTests
	{
		[TestMethod]
		public void HoriontalTests()
		{
			WordFinder wf = new WordFinder();
			wf.ReadPuzzle("puzzle0.txt");
			wf.GenerateFullPuzzle();
			wf.BuildPuzzleTree();

			/* puzzle0.txt
			 
			   ycat
			   data
			   godc
			   
			*/

			Assert.IsTrue(wf.HasWordInPuzzle("ycat"), "Didn't find horizontal word");
			Assert.IsTrue(wf.HasWordInPuzzle("data"), "Didn't find horizontal word");
			Assert.IsTrue(wf.HasWordInPuzzle("godc"), "Didn't find horizontal word");
			Assert.IsTrue(wf.HasWordInPuzzle("cat"), "Didn't find forward word");
		}

        [TestMethod]
        public void HorizontalReverseTests()
		{
			WordFinder wf = new WordFinder();
			wf.ReadPuzzle("puzzle0.txt");
			wf.GenerateFullPuzzle();
			wf.BuildPuzzleTree();

			/* puzzle0.txt
			 
			   ycat
			   data
			   godc
			   
			*/

			Assert.IsTrue(wf.HasWordInPuzzle("tacy"), "Didn't find horizontal reverse word");
			Assert.IsTrue(wf.HasWordInPuzzle("atad"), "Didn't find horizontal reverse word");
			Assert.IsTrue(wf.HasWordInPuzzle("cdog"), "Didn't find horizontal reverse word");
			Assert.IsTrue(wf.HasWordInPuzzle("tad"),  "Didn't find horizontal reverse word");
		}

        [TestMethod]
        public void DiagonalRightTests()
		{
			WordFinder wf = new WordFinder();
			wf.ReadPuzzle("puzzle0.txt");
			wf.GenerateFullPuzzle();
			wf.BuildPuzzleTree();

			/* puzzle0.txt
			 
			   ycat
			   data
			   godc
			   
			*/
			
			Assert.IsTrue(wf.HasWordInPuzzle("yad"), "Didn't find diagonal right word");
			Assert.IsTrue(wf.HasWordInPuzzle("ctc"), "Didn't find diagonal right word");
			Assert.IsTrue(wf.HasWordInPuzzle("day"), "Didn't find diagonal right reverse word");
			Assert.IsTrue(wf.HasWordInPuzzle("ctc"), "Didn't find diagonal right reverse word");
		}

        [TestMethod]
        public void DiagonalRightReverseTests()
		{
			WordFinder wf = new WordFinder();
			wf.ReadPuzzle("puzzle0.txt");
			wf.GenerateFullPuzzle();
			wf.BuildPuzzleTree();

			/* puzzle0.txt
			 
			   ycat
			   data
			   godc
			   
			*/

			
			Assert.IsTrue(wf.HasWordInPuzzle("aag"), "Didn't find diagonal left word");
			Assert.IsTrue(wf.HasWordInPuzzle("tto"), "Didn't find diagonal left word");
			Assert.IsTrue(wf.HasWordInPuzzle("gaa"), "Didn't find diagonal left reverse word");
			Assert.IsTrue(wf.HasWordInPuzzle("ott"), "Didn't find diagonal left reverse word");
		}

        [TestMethod]
        public void VerticalTests()
		{
			WordFinder wf = new WordFinder();
			wf.ReadPuzzle("puzzle0.txt");
			wf.GenerateFullPuzzle();
			wf.BuildPuzzleTree();

			/* puzzle0.txt
			 
			   ycat
			   data
			   godc
			   
			*/

			Assert.IsTrue(wf.HasWordInPuzzle("ydg"), "Didn't find vertical word");
			Assert.IsTrue(wf.HasWordInPuzzle("cao"), "Didn't find vertical word");
			Assert.IsTrue(wf.HasWordInPuzzle("atd"), "Didn't find vertical word");
			Assert.IsTrue(wf.HasWordInPuzzle("tac"), "Didn't find vertical word");
		}

        [TestMethod]
        public void VerticalReverseTests()
		{
			WordFinder wf = new WordFinder();
			wf.ReadPuzzle("puzzle0.txt");
			wf.GenerateFullPuzzle();
			wf.BuildPuzzleTree();

			/* puzzle0.txt
			 
			   ycat
			   data
			   godc
			   
			*/

			
			Assert.IsTrue(wf.HasWordInPuzzle("gdy"), "Didn't find vertical word");
			Assert.IsTrue(wf.HasWordInPuzzle("oac"), "Didn't find vertical word");
			Assert.IsTrue(wf.HasWordInPuzzle("dta"), "Didn't find vertical word");
			Assert.IsTrue(wf.HasWordInPuzzle("cat"), "Didn't find vertical word");
		}

        [TestMethod]
        public void InvalidWordTests()
		{
			WordFinder wf = new WordFinder();
			wf.ReadPuzzle("puzzle0.txt");
			wf.GenerateFullPuzzle();
			wf.BuildPuzzleTree();

			/* puzzle0.txt
			 
			   ycat
			   data
			   godc
			   
			*/

			Assert.IsFalse(wf.HasWordInPuzzle("Cat"), "Found capitalized word");
			Assert.IsFalse(wf.HasWordInPuzzle("da'ta"), "Found word with punctuation");
			Assert.IsFalse(wf.HasWordInPuzzle("at"), "Found two letter word");
			Assert.IsFalse(wf.HasWordInPuzzle(""),"found blank word");
			Assert.IsFalse(wf.HasWordInPuzzle("ycatx"),"found word longer than puzzle");			
		}

        [TestMethod]
        public void WrappingTest()
		{
			WordFinder wf = new WordFinder();
			wf.ReadPuzzle("puzzle0.txt");
			wf.GenerateFullPuzzle();
			wf.BuildPuzzleTree();

			/* puzzle0.txt
			 
			   ycat
			   data
			   godc
			   
			*/

			Assert.IsFalse(wf.HasWordInPuzzle("tda"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("tyc"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("ayc"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("dgy"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("dgc"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("dgt"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("ycd"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("oaa"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("doa"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("cyt"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("cda"),"found wrapping error");
			Assert.IsFalse(wf.HasWordInPuzzle("cta"),"found wrapping error");

		}
        
	}

	[TestClass]
	public class CharTreeTests
	{
        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
		public void TestBadCharacters()
		{
            CharTree ct = new CharTree();
			ct.Add('a');
			ct.Add('b');
			ct.Add('z');
			ct.getChild('a').Add('m');
			ct.Add('A'); // Should throw exception, because we assume valid input
		}

        [TestMethod]
        public void TestFullTree()
		{
			CharTree ct = new CharTree();
			for(char c = 'a'; c <= 'z'; c++)
			{
				ct.Add(c);
				for(char subc = 'a'; subc <= 'z'; subc++)
				{
					ct.getChild(c).Add(subc);
				}
			}
			// Should have a 2 level tree here.
			Assert.IsNotNull(ct.getChild('a'));
			Assert.IsNull(ct.getChild('a').getChild('a').getChild('a'));

			Assert.IsNotNull(ct.getChild('z'));
			Assert.IsNull(ct.getChild('z').getChild('z').getChild('z'));
		}

		[TestMethod]
		public void TestWords()
		{
			CharTree ct = new CharTree();
			ct.AddBranch("hello");
			Assert.IsTrue(ct.HasBranch("hello"),"Inserted word not found");
			Assert.IsTrue(ct.HasBranch("hell"),"part of insterted word not found");
			Assert.IsTrue(ct.HasBranch("h"),"first character of insterted word not found");

			Assert.IsFalse(ct.HasBranch("hellothere"),"inserted word, with aditional charcters found");
			Assert.IsFalse(ct.HasBranch("Hello"),"capitalized first letter found");
			Assert.IsFalse(ct.HasBranch("HELLO"),"capitalized word found");
			Assert.IsFalse(ct.HasBranch("hithere"),"word not in puzzle found");
			Assert.IsFalse(ct.HasBranch("zortblatch"),"word not in puzzle found");
		}

		[TestMethod]
		[ExpectedException(typeof(IndexOutOfRangeException))]
		public void TestWordException()
		{
			CharTree ct = new CharTree();
			ct.AddBranch("hello");
			ct.AddBranch("monkey");
			ct.AddBranch("zortblatch");

			ct.AddBranch("ka'boom");
		}
	}
}
