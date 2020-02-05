using System.Collections.Generic;
using System.Linq;

namespace SFDScript.BotExtended.Group
{
    public class GroupSet
    {
        public List<Group> Groups { get; set; }

        public GroupSet()
        {
            Groups = new List<Group>();
        }
        public bool HasBoss
        {
            get { return Groups.Where(g => g.HasBoss).Any(); }
        }

        public void AddGroup(List<SubGroup> subGroups)
        {
            Groups.Add(new Group(subGroups));
        }
    }
}
