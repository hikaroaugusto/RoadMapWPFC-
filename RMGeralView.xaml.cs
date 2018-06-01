/*
 * Created by SharpDevelop.
 * User: hikar
 * Date: 27/05/2018
 * Time: 16:45
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Data;
using System.Data.OleDb;
using System.Linq;

namespace RoadMapWPF.Views
{
	/// <summary>
	/// Interaction logic for RMGeralView.xaml
	/// </summary>
	public partial class RMGeralView : UserControl
	{
		public RMGeralView()
		{
			InitializeComponent();
			
			//Criando DataTables para os dois tipos de tabelas que temos 
			DataTable dtProjetos = new DataTable();
			DataTable dtUsuarios = new DataTable();
			
			List<itemProgressBar> items = new List<itemProgressBar>();
			
			int auxCount = 0;
			string racfAnterior = "";
			
			OleDbConnection conn = null;
			string connectionAux = @"C:\Users\hikar\Documents\Estudos_C#\Estudo_RoadMap_WPF\ProjetosUsuarios.accdb";
			string path = "Provider=Microsoft.ACE.OLEDB.12.0; data source=" + connectionAux;
				
			try{
				conn = new OleDbConnection(path);
				conn.Open();
				
				OleDbCommand cmd = new OleDbCommand("SELECT Projeto, Desenvolvedor, Peso, Escopo, Posicao FROM Projetos", conn);
				OleDbDataAdapter adapter =  new OleDbDataAdapter(cmd);
				adapter.Fill(dtProjetos);
				
				cmd = null;
				adapter = null;
				
				cmd = new OleDbCommand("SELECT Nome, Racf FROM Usuarios order by Nome", conn);
				adapter =  new OleDbDataAdapter(cmd);
				adapter.Fill(dtUsuarios);
				
				var result = from usuarios in dtUsuarios.AsEnumerable()
					join projetos in dtProjetos.AsEnumerable()
					on usuarios.Field<string>("Racf") equals projetos.Field<string>("Desenvolvedor") into eGroup
					from projetos in eGroup.DefaultIfEmpty()
					orderby usuarios.Field<string>("Nome")
					select new{
					usuariosNome = usuarios.Field<string>("Nome"),
					usuariosRacf = usuarios.Field<string>("Racf"),
					projetosProjeto = projetos == null ? "" : projetos.Field<string>("Projeto"),
					projetosDesenvolvedor = projetos == null ? "" : projetos.Field<string>("Desenvolvedor"),
					projetosPeso = projetos == null ? "" : projetos.Field<string>("Peso"),
					projetosEscopo = projetos == null ? "" : projetos.Field<string>("Escopo"),
					projetosPosicao = projetos == null ? "" : projetos.Field<string>("Posicao")
				};
				
				DataTable dtResultado = new DataTable();
				dtResultado.Columns.Add("usuariosNome");
				dtResultado.Columns.Add("usuariosRacf");
				dtResultado.Columns.Add("projetosProjeto");
				dtResultado.Columns.Add("projetosDesenvolvedor");
				dtResultado.Columns.Add("projetosPeso");
				dtResultado.Columns.Add("projetosEscopo");
				dtResultado.Columns.Add("projetosPosicao");
				
				foreach (var item in result) {
				    string posicao = item.projetosPosicao;
					int  valorPosicaoEscolhida;
				    string escopo = item.projetosEscopo;
				    string corEscopo;
				    
				  if(item.usuariosRacf != racfAnterior && auxCount != 0){
				    	int i;
				    	for(i = auxCount; i < 3; i++){
				    		items.Add(new itemProgressBar() { valueProgress = 0, textProgress = "", colorProgress = "white"});
				    	}
				    	
						auxCount = i;
				    }
				    
				    if(item.projetosDesenvolvedor == ""){
				    	for(auxCount = 0; auxCount < 3; auxCount++){
				    		items.Add(new itemProgressBar() { valueProgress = 0, textProgress = "", colorProgress = "white"});
				    	}
				    }
				    
				    if(item.usuariosRacf != racfAnterior && auxCount != 2){
				    	items.Add(new itemProgressBar() { valueProgress = 0, textProgress = "", colorProgress = "white"});
				    	auxCount = 0;
				    
				    }
				    
					switch (posicao)
					{
					    case "Entendimento":
					        valorPosicaoEscolhida = 20;
					        break;
					    case "Especificacao":
					       	valorPosicaoEscolhida = 40;
					        break; 
						case "Desenvolvimento":
					        valorPosicaoEscolhida = 60;
					        break;
					    case "Teste":
					        valorPosicaoEscolhida = 80;
					        break;
					    case "Homologacao":
					        valorPosicaoEscolhida = 90;
					        break;					        
					        
					    default:
					        valorPosicaoEscolhida = 0;
					        break;
					}
					
					
					switch (escopo)
					{
					    case "VisaoCliente":
					        corEscopo = "Red";
					        break;
					    case "Eficiencia":
					       	corEscopo = "Blue";
					        break; 
						case "Risco":
					        corEscopo = "Yellow";
					        break;
					    case "Prevencao":
					        corEscopo = "Orange";
					        break;
					    case "Inovacao":
					        corEscopo = "LightBlue";
					        break;		
					    case "Outros":
					        corEscopo = "LightGray";
					        break;										        
					        
					    default:
					        corEscopo = "White";
					        break;
					}
					
					items.Add(new itemProgressBar() { valueProgress = valorPosicaoEscolhida, textProgress = item.projetosProjeto, colorProgress = corEscopo});
					auxCount++;
				}

				icProgressBar.ItemsSource = items;				
				
			}
	        catch (Exception e)
	        {
	        	MessageBox.Show("O seguinte erro ocorreu:" + e.Message);
	        }
			
		}
	}
	
	public class itemProgressBar{
		public int valueProgress {get; set;}
		public string textProgress {get; set;}
		public string colorProgress{get;set;}
	}
}
