CREATE OR REPLACE VIEW UNIMEDLF.VW_CONTRATO_SEM_AERO AS
SELECT INF_CTR.CBN_COD_CONTRATO                      AS CODIGO_CONTRATO,
         BENEF.BEN_BEN_COD_BENEFICIARIO              AS CODIGO_BENEFICIARIO,
         PF.PSF_NOME                                 AS NOME_BENEFICIARIO,
         PES.PSS_EMAIL                               AS EMAIL_BENEFICIARIO,
         VEN1.PREMIO_ATUAL                           AS PREMIO_ATUAL,
         VW_CIDADE.NOM_CIDADE                        AS CIDADE,
         NUM_BENEF.NUM_DEPENDENTES                   AS NUMERO_DEPENDENTES
   FROM INFOMED.INF_CONTRATOS_DE_BENEFICIARIO INF_CTR
        ,INFOMED.INF_PESSOAS                  PES
        ,INF_PESSOAS_FISICAS                  PF
        ,(SELECT V.PSS_COD_PESSOA,
                 DECODE(UPPER(V.NOM_CIDADE), 'NITEROI', 'NITERÓI',
                                             'NITERÓI','NITERÓI',
                                            'SAO GONCALO', 'SÃO GONÇALO',
                                            'SÃO GONCALO', 'SÃO GONÇALO',
                                            'SÃO GONÇALO', 'SÃO GONÇALO',
                                            'ITABORAI', 'ITABORAÍ',
                                            'ITABORAÍ', 'ITABORAÍ',
                                            'RIO BONITO', 'RIO BONITO',
                                            'MARICA', 'MARICÁ',
                                            'MARICÁ', 'MARICÁ',
                                            'TANGUA', 'TANGUÁ',
                                            'TANGUÁ', 'TANGUÁ',
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
           SUM(VEN.VEN_VALOR_TOTAL_VENDA)     PREMIO_ATUAL
          FROM INFOMED.INF_VENDAS             VEN
         WHERE VEN.VEN_CANCELADA             = 'N'
           AND VEN.VEN_PRE_NUMERO_PERIODO    = TO_CHAR( ADD_MONTHS( SYSDATE, -1 ), 'YYYYMM' )
          GROUP BY VEN.VEN_CBN_COD_CONTRATO) VEN1
     WHERE PES.PSS_COD_PESSOA                = INF_CTR.CBN_PSS_COD_PESSOA
       AND VW_CIDADE.PSS_COD_PESSOA          = PES.PSS_COD_PESSOA
       AND PF.PSF_PSS_COD_PESSOA             = PES.PSS_COD_PESSOA
       AND INF_CTR.CBN_COD_CONTRATO          = VEN1.VEN_CBN_COD_CONTRATO
       AND INF_CTR.CBN_STATUS                = 'A'
       AND PES.PSS_TIPO_PESSOA               = 'F'
       AND PES.PSS_EMAIL                     IS NOT NULL
       AND BENEF.BEN_PSS_COD_PESSOA          = PES.PSS_COD_PESSOA
       AND BENEF.BEN_PLC_CBN_COD_CONTRATO    = INF_CTR.CBN_COD_CONTRATO
       AND NUM_BENEF.COD_CONTRATO            = INF_CTR.CBN_COD_CONTRATO
       AND INF_CTR.CBN_COD_CONTRATO NOT IN (
                    SELECT DISTINCT(SPC.SPC_PLC_CBN_COD_CONTRATO) COD_CONTRATO
                          FROM      INFOMED.INF_SERVICOS_EXTRA            SEX,
                                    INFOMED.INF_SERVICOS_EXTRA_PLANO_CONT SPC
                          WHERE   SEX.SEX_TLA_CODIGO_TIPO_LANCAMENTO IN ('21','23')
                            AND SEX.SEX_CODIGO_SERVICO_EXTRA = SPC.SPC_SEX_CODIGO_SERVICO_EXTRA)
  ORDER BY CBN_COD_CONTRATO;

