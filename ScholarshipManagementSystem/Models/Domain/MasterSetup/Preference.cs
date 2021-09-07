
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
        //--------------------General--------------------------
        public int DistrictSlotPopulationPer { get; set; }
        public int DistrictSlotMPIPer { get; set; }
        //--------------------Threshold--------------------------
        [Display(Name = "Matric Threshold")]
        public float MatricThreshold { get; set; }
        [Display(Name = "Intermediate Threshold")]
        public float IntermediateThreshold { get; set; }
        [Display(Name = "DAE Threshold")]
        public float DAEThreshold { get; set; }
        [Display(Name = "Graduation Threshold")]
        public float GraduationThreshold { get; set; }       
        [Display(Name = "BS Prof Threshold For Class")]
        public float BSProfThresholdForClass { get; set; }
        [Display(Name = "Bachelor Threshold For District")]
        public float BSProfDistrictThresholdFor1stY { get; set; }        
        [Display(Name = "Master Threshold")]
        public float MasterThreshold { get; set; }
        [Display(Name = "MS Threshold")]
        public float MSThreshold { get; set; }
        [Display(Name = "PhD Threshold")]
        public float PhDThreshold { get; set; }
        //--------------------Qouta In Percentage---------------------
        //---Matric
        public float Qouta { get; set; }
        [Display(Name = "POMS Matric Qouta Percentage")]
        public float POMSMatricQoutaPER { get; set; }
        [Display(Name = "DOMS Matric Qouta Percentage")]
        public float DOMSMatricQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Matric Qouta Percentage")]
        public float SQSOMSMatricQoutaPER { get; set; }
        [Display(Name = "SQS-EVI Matric Qouta Percentage")]
        public float SQSEVIMatricQoutaPER { get; set; }
        //---floatermediate
        [Display(Name = "POMS floatermediate Qouta Percentage")]
        public float POMSIntermediateQoutaPER { get; set; }
        [Display(Name = "DOMS floatermediate Qouta Percentage")]
        public float DOMSIntermediateQoutaPER { get; set; }
        [Display(Name = "SQS-OMS floatermediate Qouta Percentage")]
        public float SQSOMSIntermediateQouta { get; set; }
        [Display(Name = "SQS-EVI floatermediate Qouta Percentage")]
        public float SQSIntermediateEVIQouta { get; set; }
        //---DAE
        [Display(Name = "IOMS DAE Qouta Percentage")]
        public float IOMSDAEQoutaPER { get; set; }
        [Display(Name = "DOMS DAE Qouta Percentage")]
        public float DOMSDAEQoutaPER { get; set; }
        [Display(Name = "SQS-OMS DAE Qouta Percentage")]
        public float SQSOMSDAEQoutaPER { get; set; }
        [Display(Name = "SQS-EVI DAE Qouta Percentage")]
        public float SQSEVIDAEQoutaPER { get; set; }
        //---Graduation
        [Display(Name = "IOMS Graduation Qouta Percentage")]
        public float IOMSGraduationQoutaPER { get; set; }
        [Display(Name = "DOMS Graduation Percentage")]
        public float DOMSGraduationQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Graduation Qouta Percentage")]
        public float SQSOMSGraduationQoutaPER { get; set; }
        [Display(Name = "SQS-EVI Graduation Qouta Percentage")]
        public float SQSEVIGraduationQoutaPER { get; set; }
        //---Bachelor
        [Display(Name = "IOMS Bachelor Class Qouta Percentage")]
        public float IOMSBachelorClassQoutaPER { get; set; }
        [Display(Name = "DOMS Bachelor Class Percentage")]
        public float DOMSBachelorClassQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Bachelor Class Qouta Percentage")]
        public float SQSOMSBachelorClassQoutaPER { get; set; }
        [Display(Name = "SQS-EVI Bachelor Class Qouta Percentage")]
        public float SQSEVIBachelorClassQoutaPER { get; set; }

        [Display(Name = "IOMS Bachelor 1stY Qouta Percentage")]
        public float IOMSBachelor1stYQoutaPER { get; set; }
        [Display(Name = "DOMS Bachelor 1stY Percentage")]
        public float DOMSBachelor1stYQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Bachelor 1st Qouta Percentage")]
        public float SQSOMSBachelor1stYQoutaPER { get; set; }
        [Display(Name = "SQS-EVI Bachelor 1stY Qouta Percentage")]
        public float SQSEVIBachelor1stYQoutaPER { get; set; }
        //---Master
        [Display(Name = "IOMS Master Qouta Percentage")]
        public float IOMSMasterQoutaPER { get; set; }
        [Display(Name = "DOMS Master Percentage")]
        public float DOMSMasterQoutaPER { get; set; }
        [Display(Name = "SQS-OMS Master Qouta Percentage")]
        public float SQSOMSMasterQoutaPER { get; set; }
        [Display(Name = "SQS-EVI Master Qouta Percentage")]
        public float SQSEVIMasterQoutaPER { get; set; }
        //---MS
        [Display(Name = "IOMS MS Qouta Percentage")]
        public float IOMSMSQoutaPER { get; set; }
        [Display(Name = "DOMS MS Percentage")]
        public float DOMSMSQoutaPER { get; set; }
        [Display(Name = "SQS-OMS MS Qouta Percentage")]
        public float SQSOMSMSQoutaPER { get; set; }
        [Display(Name = "SQS-EVI MS Qouta Percentage")]
        public float SQSEVIMSQoutaPER { get; set; }
        //--------------------Qouta In Percentage---------------------
        [Display(Name = "Slot Metric")]
        public float SlotMetric { get; set; }
        [Display(Name = "Slot FAFSc 1Y")]
        public float SlotFAFSc1Y { get; set; }        
        [Display(Name = "Slot FAFSc 2Y")]
        public float SlotFAFSc2Y { get; set; }
        [Display(Name = "Slot DAE 1Y")]
        public float SlotDAE1Y { get; set; }
        [Display(Name = "Slot DAE 2Y")]
        public float SlotDAE2Y { get; set; }
        [Display(Name = "Slot Slot DAE 3Y")]
        public float SlotDAE3Y { get; set; }
        [Display(Name = "Slot Bacholar 1Y")]
        public float SlotBacholar1Y { get; set; }
        [Display(Name = "Graduation PROF 1stY Slot")]        
        public float GraduationPROF1stYSlot { get; set; }
        [Display(Name = "Graduation PROF 2ndY Slot")]
        public float GraduationPROF2ndYSlot { get; set; }
        [Display(Name = "Graduation PROF 3rdY Slot")]
        public float GraduationPROF3rdYSlot { get; set; }
        [Display(Name = "Graduation PROF 4th Slot")]
        public float GraduationPROF4thYSlot { get; set; }
        [Display(Name = "Graduation PROF 5th Slot")]
        public float GraduationPROF5thYSlot { get; set; }
        [Display(Name = "Slot Master")]
        public float MasterSlot { get; set; }
        [Display(Name = "Slot MS")]
        public float MSSlot { get; set; }
    }
}
