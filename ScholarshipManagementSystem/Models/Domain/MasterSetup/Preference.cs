
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace ScholarshipManagementSystem.Models.Domain.MasterSetup
{
    [Table("Preference", Schema = "master")]
    public class Preference
    {
        [Key]
        public int PreferenceId { get; set; }
        //--------------------STIPEND--------------------------
        [Required]
        [Display(Name = "Scheme Matric Stipend")]
        public int SchemeMatricStipend { get; set; }
        [Display(Name = "Scheme Intermediate Stipend")]
        public int SchemeIntermediateStipend { get; set; }
        [Display(Name = "Scheme DAE Stipend")]
        public int SchemeDAEStipend { get; set; }
        [Display(Name = "Scheme Graduation Stipend")]
        public int SchemeGraduationStipend { get; set; }
        [Display(Name = "Scheme Bacholar Stipend")]
        public int SchemeBacholarStipend { get; set; }
        [Display(Name = "Scheme Master Stipend")]
        public int SchemeMasterStipend { get; set; }
        [Display(Name = "Scheme MS Stipend")]
        public int SchemeMSStipend { get; set; }
        //--------------------Threshold--------------------------
        [Display(Name = "Matric Threshold")]
        public int MatricThreshold { get; set; }
        [Display(Name = "Intermediate Threshold")]
        public int IntermediateThreshold { get; set; }
        [Display(Name = "DAE Threshold")]
        public int DAEThreshold { get; set; }
        [Display(Name = "Graduation Threshold")]
        public int GraduationThreshold { get; set; }
        [Display(Name = "BS Prof Threshold For Class")]
        public int BSProfThresholdForClass { get; set; }
        [Display(Name = "bachelor Threshold")]
        public int BSProfDistrictThresholdFor1stY { get; set; }        
        [Display(Name = "Master Threshold")]
        public int MasterThreshold { get; set; }
        [Display(Name = "MS Threshold")]
        public int MSThreshold { get; set; }
        [Display(Name = "PhD Threshold")]
        public int PhDThreshold { get; set; }
        //--------------------Qouta In Percentage---------------------
        //---Matric
        public int Qouta { get; set; }
        [Display(Name = "POMS Matric Qouta Percentage")]
        public int POMSMatricQoutaPER { get; set; }
        [Display(Name = "DOMS Matric Qouta Percentage")]
        public int DOMSMatricQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Matric Qouta Percentage")]
        public int SQSOMSMatricQoutaPER { get; set; }
        [Display(Name = "SQS-EVI Matric Qouta Percentage")]
        public int SQSEVIMatricQoutaPER { get; set; }
        //---Intermediate
        [Display(Name = "POMS Intermediate Qouta Percentage")]
        public int POMSIntermediateQoutaPER { get; set; }
        [Display(Name = "DOMS Intermediate Qouta Percentage")]
        public int DOMSIntermediateQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Intermediate Qouta Percentage")]
        public int SQSOMSIntermediateQouta { get; set; }
        [Display(Name = "SQS-EVI Intermediate Qouta Percentage")]
        public int SQSIntermediateEVIQouta { get; set; }
        //---DAE
        [Display(Name = "IOMS DAE Qouta Percentage")]
        public int IOMSDAEQoutaPER { get; set; }
        [Display(Name = "DOMS DAE Qouta Percentage")]
        public int DOMSDAEQoutaPER { get; set; }
        [Display(Name = "SQS-OMS DAE Qouta Percentage")]
        public int SQSOMSDAEQoutaPER { get; set; }
        [Display(Name = "SQS-EVI DAE Qouta Percentage")]
        public int SQSEVIDAEQoutaPER { get; set; }
        //---Graduation
        [Display(Name = "POMS Graduation Qouta Percentage")]
        public int POMSGraduationQoutaPER { get; set; }
        [Display(Name = "DOMS Graduation Percentage")]
        public int DOMSGraduationQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Graduation Qouta Percentage")]
        public int SQSOMSGraduationQoutaPER { get; set; }
        [Display(Name = "SQS-EVI Graduation Qouta Percentage")]
        public int SQSEVIGraduationQoutaPER { get; set; }
        //---Bachelor
        [Display(Name = "POMS Bachelor Qouta Percentage")]
        public int POMSBachelorQoutaPER { get; set; }
        [Display(Name = "DOMS Bachelor Percentage")]
        public int DOMSBachelorQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Bachelor Qouta Percentage")]
        public int SQSOMSBachelorQoutaPER { get; set; }
        [Display(Name = "SQS-EVI Bachelor Qouta Percentage")]
        public int SQSEVIBachelorQoutaPER { get; set; }
        //---Master
        [Display(Name = "POMS Master Qouta Percentage")]
        public int POMSMasterQoutaPER { get; set; }
        [Display(Name = "DOMS Master Percentage")]
        public int DOMSMasterQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Master Qouta Percentage")]
        public int SQSOMSMasterQoutaPER { get; set; }
        [Display(Name = "SQS-EVI Master Qouta Percentage")]
        public int SQSEVIMasterQoutaPER { get; set; }
        //---MS
        [Display(Name = "POMS MS Qouta Percentage")]
        public int POMSMSQoutaPER { get; set; }
        [Display(Name = "DOMS MS Percentage")]
        public int DOMSMSQoutaPER { get; set; }
        [Display(Name = "SQS-OMS MS Qouta Percentage")]
        public int SQSOMSMSQoutaPER { get; set; }
        [Display(Name = "SQS-EVI MS Qouta Percentage")]
        public int SQSEVIMSQoutaPER { get; set; }
        //--------------------Qouta In Percentage---------------------
        [Display(Name = "Slot Metric")]
        public int SlotMetric { get; set; }
        [Display(Name = "Slot FAFSc 1Y")]
        public int SlotFAFSc1Y { get; set; }        
        [Display(Name = "Slot FAFSc 2Y")]
        public int SlotFAFSc2Y { get; set; }
        [Display(Name = "Slot DAE 1Y")]
        public int SlotDAE1Y { get; set; }
        [Display(Name = "Slot DAE 2Y")]
        public int SlotDAE2Y { get; set; }
        [Display(Name = "Slot Slot DAE 3Y")]
        public int SlotDAE3Y { get; set; }
        [Display(Name = "Slot Bacholar 1Y")]
        public int SlotBacholar1Y { get; set; }
        [Display(Name = "Slot Bacholar")]
        public int BacholarSlot { get; set; }
        [Display(Name = "Slot Master")]
        public int MasterSlot { get; set; }
        [Display(Name = "Slot MS")]
        public int MSSlot { get; set; }
    }
}
