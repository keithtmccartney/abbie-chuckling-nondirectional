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

        IList<Transaction> PendingTransactions = new List<Transaction>(); //The new block generating process is a time-consuming process. But, a transaction can be submitted anytime, so we need to have a place to store transactions before they are processed. Therefore, we added a new field, PendingTransactions, to store newly added transactions.

        public int Difficulty { set; get; } = 2; //The Blockchain class is updated to have a new field Difficulty.

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

        /// <summary>
        /// The new block generating process is updated to add a mining step.
        /// </summary>
        /// <param name="block"></param>
        public void AddBlock(Block block)
        {
            Block latestBlock = GetLatestBlock();

            block.Index = latestBlock.Index + 1;
            block.PreviousHash = latestBlock.Hash;
            block.Mine(this.Difficulty);

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

        /// <summary>
        /// We added a CreateTransaction method to add a new transaction to the PendingTransaction collection.
        /// </summary>
        /// <param name="transaction"></param>
        public void CreateTransaction(Transaction transaction)
        {
            PendingTransactions.Add(transaction);
        }

        /// <summary>
        /// We need to update Blockchain and add a ProcessPendingTransactions method.
        /// This method needs a miner address as the parameter.
        /// </summary>
        /// <param name="minerAddress"></param>
        public void ProcessPendingTransactions(string minerAddress)
        {
            Block block = new Block(DateTime.Now, GetLatestBlock().Hash, PendingTransactions);

            AddBlock(block);

            PendingTransactions = new List<Transaction>();

            CreateTransaction(new Transaction(null, minerAddress, Reward));
        }
    }
}
