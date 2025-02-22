using Lms;
using Lms.Models;
using ConsoleTables;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Collections;
using System.Windows.Markup;
using Microsoft.EntityFrameworkCore.Infrastructure;


namespace Lms.Controllers
{
    [Cli.Controller]
    class Block {
        private LmsDbContext? db;

        public Block(LmsDbContext db) {
            this.db = db;
        }

        [Cli.Verb]
        public Models.Block Create(string[] args) {
            // when user enter invalid command just throw excpetion
            if(args.Length == 1 || args.Length > 2) {
                throw new ArgumentException("Invalid options.");
            }

            Models.Block newBlock = new Models.Block();

            // args.Length == 2 means user enters right command
            // user also enter without args which means args.Length == 0
            if(args.Length == 2) {
                // to check if the description is null
                if(args[0] != "-") {
                    newBlock.Description = args[0];
                }

                // to check if the work item id is null
                if(args[1] != "-") {
                    newBlock.WorkItems = ConvertWorkItemList(args, 1);
                }
            }

            db.Block.Add(newBlock);
            db.SaveChanges();

            return newBlock;
        }

        [Cli.Verb]
        public IEnumerable<Models.Block> List() {
            return db.Block.ToList();
        }

        private List<Lms.Models.WorkItem> ConvertWorkItemList(string[] args, int argumentIndex) {
            List<string> workItemIds = args[argumentIndex].Split(',').ToList();
            List<Lms.Models.WorkItem> workItems = new List<Lms.Models.WorkItem>();
            foreach(string workitemid in workItemIds) {
                if(int.TryParse(workitemid, out int id)) {
                    Lms.Models.WorkItem workItem = db.WorkItems.Where(w => w.Id == Convert.ToInt32(id)).FirstOrDefault();
                    if(workItem != null) {
                        workItems.Add(workItem);
                    }
                }
                else {
                    Console.WriteLine($"Wrong work item id, {workitemid}, is entered.");
                }
            }
            return workItems;
        }

        [Cli.Verb]
        public Models.Block Edit(string[] args) {
            // when user enter invalid command just throw excpetion
            if(args.Length != 3) {
                throw new ArgumentException("Invalid options.");
            }

            // when user enter invalid type for blockId
            if(!int.TryParse(args[0], out int blockId)) {
                throw new ArgumentException("Invalid block id.");
            }

            // when user enter non-existing blockId
            Models.Block existBlock = db.Block.Where(b => b.Id == blockId).FirstOrDefault();
            if(existBlock == null) {
                throw new ArgumentException("Invalid block id.");
            }

            // to check if the description is null
            if(args[1] == "-") {
                existBlock.Description = null;
            }else {
                existBlock.Description = args[1];
            }

            // to check if the work item id is null
            if(args[2] == "-") {
                existBlock.WorkItems = new List<Lms.Models.WorkItem>();
            }else {
                existBlock.WorkItems = ConvertWorkItemList(args, 2);
            }
            db.SaveChanges();

            return existBlock;
        }

        [Cli.Verb]
        public Models.Block Delete(string[] args) {
            // when user enter invalid command just throw excpetion
            Console.WriteLine(args.Length);
            if(args.Length != 1) {
                throw new ArgumentException("Invalid options.");
            }

            // when user enter invalid type for blockId
            if(!int.TryParse(args[0], out int blockIdToRemove)) {
                throw new ArgumentException("Invalid block id.");
            }

            // when user enter non-existing blockId
            Models.Block blockToRemove = db.Block.Where(b => b.Id == blockIdToRemove).FirstOrDefault();
            if(blockToRemove == null) {
                throw new ArgumentException("Invalid block id.");
            }

            db.Block.Remove((Models.Block)blockToRemove);
            db.SaveChanges();

            return blockToRemove;
        }

        public IEnumerable<Models.Block> GetBlockers()
        {
            return db.Block.AsEnumerable();
        }
    }
}
