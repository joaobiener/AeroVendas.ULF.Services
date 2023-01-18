using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


 namespace Entities.Models;

/*
CREATE OR REPLACE VIEW UNIMEDLF.VW_CONTRATO_SEM_AERO AS
SELECT INF_CTR.CBN_COD_CONTRATO                      AS CODIGO_CONTRATO,
         BENEF.BEN_BEN_COD_BENEFICIARIO              AS CODIGO_BENEFICIARIO,
         PF.PSF_NOME                                 AS NOME_BENEFICIARIO,
         PES.PSS_EMAIL                               AS EMAIL_BENEFICIARIO,
         VEN1.PREMIO_ANUAL                           AS PREMIO_ANUAL,
         VW_CIDADE.NOM_CIDADE                        AS CIDADE,
         NUM_BENEF.NUM_DEPENDENTES                   AS NUMERO_DEPENDENTES
   FROM INFOMED.INF_CONTRATOS_DE_BENEFICIARIO INF_CTR
        ,INFOMED.INF_PESSOAS                  PES
        ,INF_PESSOAS_FISICAS                  PF
        ,(SELECT V.PSS_COD_PESSOA,
                 DECODE(UPPER(V.NOM_CIDADE), 'NITEROI', 'NITERÓI',
                                            'SAO GONCALO', 'SÃO GONÇALO',
                                            'ITABORAI', 'ITABORAÍ',
                                            'RIO BONITO', 'RIO BONITO',
                                            'MARICA', 'MARICÁ',
                                            'TANGUA', 'TANGUÁ',
                                            'SILVA JARDIM', 'SILVA JARDIM',
                                            'OUTROS') NOM_CIDADE
            FROM VW_ENDERECO_PESSOA V
           WHERE EPE_CORRESP = 'S'    )   VW_CIDADE
        ,INFOMED.INF_BENEFICIARIOS            BENEF
        ,(SELECT INF.BEN_PLC_CBN_COD_CONTRATO COD_CONTRATO,
                COUNT(1) AS NUM_DEPENDENTES
           FROM INF_BENEFICIARIOS INF
       GROUP BY INF.BEN_PLC_CBN_COD_CONTRATO) NUM_BENEF
        ,(SELECT VEN.VEN_CBN_COD_CONTRATO,
           SUM(VEN.VEN_VALOR_TOTAL_VENDA)     PREMIO_ANUAL
          FROM INFOMED.INF_VENDAS             VEN
         WHERE VEN.VEN_CANCELADA             = 'N'
           AND VEN.VEN_PRE_NUMERO_PERIODO    >= TO_CHAR( add_months( SYSDATE, -12 ), 'YYYYMM' )
          GROUP BY VEN.VEN_CBN_COD_CONTRATO) VEN1
     WHERE PES.PSS_COD_PESSOA                = INF_CTR.CBN_PSS_COD_PESSOA
       AND VW_CIDADE.PSS_COD_PESSOA          = PES.PSS_COD_PESSOA
       AND PF.PSF_PSS_COD_PESSOA             = PES.PSS_COD_PESSOA
       AND INF_CTR.CBN_COD_CONTRATO          = VEN1.VEN_CBN_COD_CONTRATO
       AND INF_CTR.CBN_STATUS                = 'A'
       AND PES.PSS_TIPO_PESSOA               = 'F'
       AND PES.PSS_EMAIL                     IS NOT NULL
       AND BENEF.BEN_PSS_COD_PESSOA          = PES.PSS_COD_PESSOA
       AND NUM_BENEF.COD_CONTRATO            = INF_CTR.CBN_COD_CONTRATO
       AND INF_CTR.CBN_COD_CONTRATO NOT IN (
                    SELECT    DISTINCT(cbn.cbn_cod_contrato)
                    FROM      INFOMED.INF_SERVICOS_EXTRA            SEX
                    JOIN      INFOMED.INF_SERVICOS_EXTRA_PLANO_CONT SPC ON  SPC.SPC_SEX_CODIGO_SERVICO_EXTRA = SEX.SEX_CODIGO_SERVICO_EXTRA
                    JOIN      INFOMED.INF_PLANOS_DO_CONTRATO        PLC ON  PLC.PLC_CBN_COD_CONTRATO         = SPC.SPC_PLC_CBN_COD_CONTRATO
                                                                        AND PLC.PLC_COD_PLANO                = SPC.SPC_PLC_COD_PLANO
                    JOIN      INFOMED.INF_CONTRATOS_DE_BENEFICIARIO CBN ON  CBN.CBN_COD_CONTRATO             = PLC.PLC_CBN_COD_CONTRATO
                    WHERE     CBN.CBN_STATUS = 'A'
                    AND       SEX.SEX_TLA_CODIGO_TIPO_LANCAMENTO IN ('21','23'))
  ORDER BY CBN_COD_CONTRATO;
*/

[Table("VW_CONTRATO_SEM_AERO", Schema = "UNIMEDLF")]
    [Keyless]
    public class ViewContratoSemAeroVendas
    {

        [Column("CODIGO_CONTRATO")]
        public string? Contrato { get; set; }

        [Column("CODIGO_BENEFICIARIO")]
        public string? CodigoBeneficiario { get; set; }

        [Column("NOME_BENEFICIARIO")]
        public string? NomeBeneficiario { get; set; }

        [Column("EMAIL_BENEFICIARIO")]
        public string? EmailBeneficiario { get; set; }

        [Column("PREMIO_ANUAL")]
        public double PremioAnual { get; set; }

        [Column("CIDADE")]
        public string? Cidade{ get; set; }

        [Column("NUMERO_DEPENDENTES")]
        public int? NumeroDependentes { get; set; }
  

}
