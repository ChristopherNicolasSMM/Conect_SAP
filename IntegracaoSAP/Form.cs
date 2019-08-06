using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SAP.Middleware.Connector;  // SAP .NET Connector
using Newtonsoft.Json;
using IntegracaoSAP.Classes;
using System.Data.SqlClient;

namespace IntegracaoSAP
{
    public partial class Form : System.Windows.Forms.Form
    {
        public Form()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            get_integracao();
        }


        public byte[] IntToBCD(int input)
        {
            byte[] bcd = new byte[] {
                (byte)(input>> 8),
                (byte)(input& 0x00FF)
            };
            return bcd;
        }

        public void get_integracao()
        {
            List<Classes.ParametroRFC> parametros = new List<Classes.ParametroRFC> {
                new Classes.ParametroRFC(){ TIPO_REGISTRO = "1" },
                new Classes.ParametroRFC(){ TIPO_REGISTRO = "2" },
                new Classes.ParametroRFC(){ TIPO_REGISTRO = "3" }
            };

            // Cria parametros de conexão
            RfcConfigParameters meusap = new RfcConfigParameters();

            // Alimenta parametros SAP Logon
            meusap.Add(RfcConfigParameters.Name, txt_name.Text);
            meusap.Add(RfcConfigParameters.AppServerHost, txt_host.Text);
            meusap.Add(RfcConfigParameters.SystemNumber, txt_number.Text);

            // Alimenta parametros SAP GUI
            meusap.Add(RfcConfigParameters.Client, txt_client.Text);
            meusap.Add(RfcConfigParameters.User, txt_user.Text);
            meusap.Add(RfcConfigParameters.Password, txt_pass.Text);
            meusap.Add(RfcConfigParameters.SAPRouter, txt_sao_router.Text);

            // Cria destino
            RfcDestination meurfc = RfcDestinationManager.GetDestination(meusap);

            try
            {
                if (meurfc != null)
                {
                    // Verifica conexão
                    meurfc.Ping();
                    lbl_status.Text = "SAP - Conectado com sucesso.";

                    // Lê tabela
                    RfcRepository repo = meurfc.Repository;

                    // Define função de exportação de textos
                    IRfcFunction funcaoRT = repo.CreateFunction("ZSD_RFC_INTEGRACAO");

                    // Define parametros da função
                    IRfcTable tab_parametro = funcaoRT.GetTable("T_PARAMETER");

                    IRfcStructure p_estrutura = funcaoRT.GetStructure("P_ESTRUTURA");

                    foreach (Classes.ParametroRFC item in parametros)
                    {
                        tab_parametro.Insert();

                        tab_parametro[tab_parametro.CurrentIndex].SetValue("TIPO_REGISTRO", item.TIPO_REGISTRO.ToString());

                        p_estrutura.SetValue("TIPO_REGISTRO", item.TIPO_REGISTRO.ToString());
                    }

                    //funcaoRT.SetValue("TABELA", parametros);

                    // Chama função
                    funcaoRT.Invoke(meurfc);

                    // Recupera Dados cru, você precisa trata-los para
                    // que sejam humanamente legivelif
                    IRfcStructure tabela = funcaoRT.GetStructure("P_RETURN");

                    // Recupera texto
                    string dados = tabela.GetValue(3).ToString();

                    // Workaround para limpar tabela
                    // Existem métodos mais eficientes
                    // Este é apenas um caminho rápido para exemplo
                    // Também há possibilidades em diferentes funções e BAPis
                    // Converter um set de dados da função em um set de dados .NET
                    dados = dados.Replace("TABLE  [STRUCTURE TAB512 { FIELD WA=", "");
                    dados = dados.Replace("[STRUCTURE TAB512 { FIELD WA=", "");
                    dados = dados.Replace("}]", "\n");

                    // alimenta GUI
                    txt_return.AppendText(dados.TrimStart());
                }
            }
            catch (Exception ex)
            {
                // Erro ao conectar
                lbl_status.Text = "SAP - Problema de Conexão";
                MessageBox.Show(ex.Message.ToString(), "Erro",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
