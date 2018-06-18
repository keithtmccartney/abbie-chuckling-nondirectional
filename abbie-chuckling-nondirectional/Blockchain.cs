using System;
using System.Collections.Generic;
using System.Text;

namespace abbie_chuckling_nondirectional
{
    /// <summary>
    /// Blockchain is a database.
    /// What is a database? A database is an organized collection of data.
    /// Or, you can say, a data structure that stores the data.
    /// Therefore, Blockchain is just a data structure that stores the data.
    /// </summary>
    public class Blockchain
    {
        public IList<Block> Chain { set; get; }

        public Blockchain()
        {
            InitializeChain();

            AddGenesisBlock();
        }

        public void InitializeChain()
        {
            Chain = new List<Block>();
        }

        /// <summary>
        /// The first block is a special block: genesis block.
        /// Genesis block is the only block that has no previous blocks and does not contain data.
        /// </summary>
        /// <returns></returns>
        public Block CreateGenesisBlock()
        {
            return new Block(DateTime.Now, null, "{}");
        }

        public void AddGenesisBlock()
        {
            Chain.Add(CreateGenesisBlock());
        }

        public Block GetLatestBlock()
        {
            return Chain[Chain.Count - 1];
        }

        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();

            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Hash = block.CalculateHash();

            Chain.Add(block);
        }

        /// <summary>
        /// The IsValid method will check two things.
        /// - Each block’s hash to see if the block is changed
        /// - Previous block’s hash to see if the block is changed and recalculated
        /// </summary>
        /// <returns></returns>
        public bool IsValid()
        {
            for (int i = 1; i < Chain.Count; i++)
            {
                Block currentBlock = Chain[i];
                Block previousBlock = Chain[i - 1];

                if (currentBlock.Hash != currentBlock.CalculateHash())
                {
                    return false;
                }

                if (currentBlock.PreviousHash != previousBlock.Hash)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
