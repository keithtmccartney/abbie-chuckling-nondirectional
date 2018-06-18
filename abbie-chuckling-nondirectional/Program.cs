using Newtonsoft.Json;
using System;

namespace abbie_chuckling_nondirectional
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain phillyCoin = new Blockchain();

            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Henry,receiver:MaHesh,amount:10}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:MaHesh,receiver:Henry,amount:5}"));
            phillyCoin.AddBlock(new Block(DateTime.Now, null, "{sender:Mahesh,receiver:Henry,amount:5}"));

            #region One of the advantages of using blockchain is data security. Data security means that tampering with the old data and altering the method of securing new data is prevented by both the cryptographic method and the non-centralized storage of the data itself. However, blockchain is just a data structure in which data can be easily changed like this. Therefore, we need a way to validate the data. This is why I have added an IsValid method to the code.
            //phillyCoin.Chain[1].Data = "{sender:Henry,receiver:MaHesh,amount:1000}";
            #endregion

            #region The Blockchain information will be serialized into JSON and the output in console.
            Console.WriteLine(JsonConvert.SerializeObject(phillyCoin, Formatting.Indented));
            #endregion

            #region Then, we call the IsValid before the data tampering and after the data tampering to see if there are any data issues.
            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            Console.WriteLine($"Update amount to 1000");

            phillyCoin.Chain[1].Data = "{sender:Henry,receiver:MaHesh,amount:1000}";

            Console.WriteLine($"Is Chain Valid: {phillyCoin.IsValid()}");
            #endregion

            #region How about the case when the attacker recalculates the hash of the tampered block? The Validation result will still be false because the validation not only looks at the current block, but also at the link to the previous block.
            //phillyCoin.Chain[1].Hash = phillyCoin.Chain[1].CalculateHash();
            #endregion

            #region Now, what about the case when an attacker recalculates hashes of all the current block and the following blocks? After all the Blocks are recalculated, the verification is passed. However, this is only passed on one node because Blockchain is a decentralized system. Tampering with one node could be easy but tampering with all the nodes in the system is impossible.
            //Console.WriteLine($"Update the entire chain");

            //phillyCoin.Chain[2].PreviousHash = phillyCoin.Chain[1].Hash;
            //phillyCoin.Chain[2].Hash = phillyCoin.Chain[2].CalculateHash();
            //phillyCoin.Chain[3].PreviousHash = phillyCoin.Chain[2].Hash;
            //phillyCoin.Chain[3].Hash = phillyCoin.Chain[3].CalculateHash();
            #endregion
        }
    }
}
