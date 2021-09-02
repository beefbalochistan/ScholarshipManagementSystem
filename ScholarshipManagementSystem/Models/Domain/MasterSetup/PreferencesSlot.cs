
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("PreferencesSlot", Schema = "master")]
    public class PreferencesSlot
    {
        [Key]
        public int PreferencesSlotId { get; set; }
        public int SlotMetric { get; set; }
        public int SlotFAFSc1Y { get; set; }
        public int SlotFAFSc2Y { get; set; }
        public int SlotDAE1Y { get; set; }
        public int SlotDAE2Y { get; set; }
        public int SlotDAE3Y { get; set; }
        public int SlotBacholar1Y { get; set; }
        public int BacholarSlot { get; set; }
        public int MasterSlot { get; set; }
    }
}
