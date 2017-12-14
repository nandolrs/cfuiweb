using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;

using System.Globalization;
using System.Threading;

using cf.dados;
using cf.util;
using System.ComponentModel.DataAnnotations.Schema;


namespace cf.ui.web
{
    public static class CFLABEL
    {
        public const string usuarioNome = "usuarioNome";
        public const string LB_SELECAO = "LB_SELECAO";

    }

    public class Pagina : System.Web.UI.Page
    {
        cf.util.Biblioteca oBiblioteca;

        cf.ui.web.PaginaComum _paginaComum;

        public string literalmente(string chave)
        {
            string sRetorno = "";
            string sLinguagem = "pt-BR";
            string[] sLinguagens = HttpContext.Current.Request.UserLanguages;

            if (sLinguagens.Length > 0)
            {
                sLinguagem = sLinguagens[0];
            }

            cf.util.Biblioteca oLib = new util.Biblioteca();
            string sXml = oLib.obtemRecursoTexto("CUTIL.RECURSOS.XmlLanguage.xml");
            System.Xml.XmlDocument oXmlDocument = new System.Xml.XmlDocument();
            oXmlDocument.LoadXml(sXml);

            System.Xml.XmlNode oXmlNode = oXmlDocument.SelectSingleNode("/language/frase [@idioma='"+sLinguagem+"' and @chave='" + chave + "']/@text");
            sRetorno = oXmlNode.InnerText;

            return sRetorno;
        }

        public Pagina()
        {
            oBiblioteca = new util.Biblioteca();

            _paginaComum = new PaginaComum(this);

            _paginaComum.EVENTOcontexto += _paginaComum_EVENTOcontexto;
        }

        private object _paginaComum_EVENTOcontexto()
        {
            return this;
        }

        //protected void configuraLinguagem(string linguagemISO)
        //{
        //    if (CultureInfo.CurrentUICulture.Name != linguagemISO)
        //    {
        //        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(linguagemISO);
        //        Thread.CurrentThread.CurrentUICulture = new CultureInfo(linguagemISO);
        //    }
        //}

        protected virtual string buscartitulo()
        {
            return "";
        }

        protected string titulo()
        {
            string mensagem = buscartitulo();
            return literalmente(mensagem);
        }

        protected cf.comum.enumeracoes.CFMODO_MANUTENCAO modoOperacao()
        {
            cf.comum.enumeracoes.CFMODO_MANUTENCAO retorno = comum.enumeracoes.CFMODO_MANUTENCAO.nenhum;
            if (Request["codigo"] != null)
            {
                int nCodigo = int.Parse(Request["codigo"]);

                if (nCodigo == 0)
                {
                    retorno = cf.comum.enumeracoes.CFMODO_MANUTENCAO.incluindo;
                }
                else
                {
                    retorno = cf.comum.enumeracoes.CFMODO_MANUTENCAO.alterando;
                }

            }

            return retorno;
        }
    }

    public class PaginaP : Pagina
    {
        cf.dados.Entidade _entidade;

        cf.dados.EntidadeBase _entidadeBase;

        cf.ui.web.PaginaComum _paginaComum;

        DataGrid _grade;



        public PaginaP()
        {
            _paginaComum = new PaginaComum(this);

            _paginaComum.EVENTOcontexto += _paginaComum_EVENTOcontexto;
        }

        private object _paginaComum_EVENTOcontexto()
        {
            return this;
        }

        protected virtual DataGrid buscaGrade()
        {
            DataGrid retorno = null;

            return retorno;
        }

        protected virtual cf.dados.Entidade buscaEntidade()
        {
            cf.dados.Entidade retorno = null;

            return retorno;
        }

        protected virtual cf.dados.EntidadeBase buscaEntidadeBase()
        {
            cf.dados.Entidade retorno = null;

            return retorno;
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            _paginaComum.credenciaisVerificar();




            if (!IsPostBack)
            {


#if ENTITY
                _entidadeBase = buscaEntidadeBase();
                _grade = buscaGrade();
                _grade.Visible = false;


                //_entidadeBase.pesquisar();
                //DataGrid grdListaEntidade = buscaGrade();

                //grdListaEntidade.DataSource = _entidadeBase.lista;
                //grdListaEntidade.DataBind();
#else
                _entidade = buscaEntidade();

                //_entidade.pesquisar();
                //DataGrid grdListaEntidade = buscaGrade();

                //grdListaEntidade.DataSource = _entidade;
                //grdListaEntidade.DataBind();

#endif

                //if (_entidade != null)
                //{

                //    if (_entidade != null)
                //    {
                //        _entidade.pesquisar();
                //        DataGrid grdListaEntidade = buscaGrade();

                //        grdListaEntidade.DataSource = _entidade;
                //        grdListaEntidade.DataBind();
                //    }
                //}
                //else if (_entidadeBase != null)
                //{

                //    _entidadeBase.pesquisar();
                //    DataGrid grdListaEntidade = buscaGrade();

                //    grdListaEntidade.DataSource = _entidadeBase;
                //    grdListaEntidade.DataBind();
                //}

            }
        }

        protected void input_Click(object sender, EventArgs e)
        {
#if ENTITY
            _paginaComum.entidadeNome = buscaEntidadeBase().ToString().ToLower();
            _paginaComum.entidadeBase = buscaEntidadeBase();
            _paginaComum.grade = buscaGrade();

#else
            _paginaComum.entidadeNome = buscaEntidade().ToString().ToLower();
            _paginaComum.entidade = buscaEntidade();

#endif

            _paginaComum.input_Click(sender, e);
        }


    }

    public class PaginaM : Pagina
    {
        cf.dados.Entidade _entidade;

        cf.dados.EntidadeBase _entidadeBase;

        cf.ui.web.PaginaComum _paginaComum;

        public PaginaM()
        {
            _paginaComum = new PaginaComum(this);

            _paginaComum.EVENTOcontexto += _paginaComum_EVENTOcontexto;

        }

        private object _paginaComum_EVENTOcontexto()
        {
            return this;
        }

        protected virtual cf.dados.Entidade buscaEntidade()
        {
            cf.dados.Entidade retorno=null;

            return retorno;
        }

        protected virtual cf.dados.EntidadeBase buscaEntidadeEntity()
        {
            cf.dados.EntidadeBase retorno = null;

            return retorno;
        }

        protected virtual cf.dados.EntidadeBase buscaEntidadeBase()
        {
            cf.dados.EntidadeBase retorno = null;

            return retorno;
        }

        //        protected void Page_Load(object sender, EventArgs e)
        //        {
        //            _paginaComum.credenciaisVerificar();

        //            if (!IsPostBack)
        //            {

        //                if (Request["codigo"] != null)
        //                {
        //                    int nCodigo = int.Parse(Request["codigo"]);

        //                    if (nCodigo != 0) // alterar
        //                    {
        //#if ENTITY
        //                        _entidadeBase = buscaEntidadeBase();
        //                        _entidadeBase.codigo = nCodigo;
        //                        _entidadeBase.consultar();
        //                        //_paginaComum.carregarEntity(_entidadeBase);
        //                        _paginaComum.transportaEntity(_entidadeBase, comum.enumeracoes.CFSENTIDO.obj2ui);
        //#else
        //                        _entidade = buscaEntidade();
        //                        _entidade.codigo = nCodigo;
        //                        _entidade.consultar();        
        //                        _paginaComum.carregar(_entidade);                                                      
        //#endif



        //                    } // incluir
        //                }
        //            }
        //        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _paginaComum.credenciaisVerificar();

            if (!IsPostBack)
            {

               // if (modoOperacao() == comum.enumeracoes.CFMODO_MANUTENCAO.alterando)
               // {
              

                    if (modoOperacao() == comum.enumeracoes.CFMODO_MANUTENCAO.alterando) // alterar
                    {
#if ENTITY
                        int nCodigo = int.Parse(Request["codigo"]);

                        _entidadeBase = buscaEntidadeBase();
                        _entidadeBase.codigo = nCodigo;
                        _entidadeBase.consultar(true);
                        //_paginaComum.carregarEntity(_entidadeBase);
                        _paginaComum.transportaEntity(_entidadeBase, comum.enumeracoes.CFSENTIDO.obj2ui);
#else
                        _entidade = buscaEntidade();
                        _entidade.codigo = nCodigo;
                        _entidade.consultar();        
                        _paginaComum.carregar(_entidade);                                                      
#endif



                    } // incluir
                //}
            }
        }


        protected void input_Click(object sender, EventArgs e)
        {
#if ENTITY
            _paginaComum.entidadeBase = buscaEntidadeBase();
            _paginaComum.entidadeNome = _paginaComum.entidadeBase.ToString().ToLower();
#else
            _paginaComum.entidadeNome = buscaEntidade().ToString().ToLower();
            _paginaComum.entidade = buscaEntidade();
#endif

            _paginaComum.input_Click(sender, e);
        }

        protected override void InitializeCulture()
        {
            string sLinguagem = _paginaComum.credenciaisLinguagemObter();

            if (sLinguagem != null && sLinguagem != "")
            {
                cf.util.BibliotecaStatic.configuraLinguagem(sLinguagem);

            }

            base.InitializeCulture();
        }

    }

    public class PaginaMasterLogin : System.Web.UI.MasterPage
    {
        cf.ui.web.PaginaComum _paginaComum;

        DropDownList inputBandeiras;

        public PaginaMasterLogin()
        {
            _paginaComum = new PaginaComum(this);

            _paginaComum.EVENTOcontexto += _paginaComum_EVENTOcontexto;

            this.Load += PaginaMasterLogin_Load;
        }

        private void PaginaMasterLogin_Load(object sender, EventArgs e)
        {
            inputBandeiras = (DropDownList)FindControl("inputBandeiras");


            
        }

        private object _paginaComum_EVENTOcontexto()
        {
            return this;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            inputBandeiras = (DropDownList) FindControl("inputBandeiras");

            if (!IsPostBack)
            {
                string linguagem =    _paginaComum.credenciaisLinguagemObter();
                if (linguagem != null)
                {
                    inputBandeiras.SelectedValue = linguagem;
                }
            }
        }

        protected void input_Click(object sender, EventArgs e)
        {
            _paginaComum.input_Click(sender, e);
        }

    }

    public class PaginaMaster : System.Web.UI.MasterPage
    {
        Menu inputMenu;

        cf.ui.web.PaginaComum _paginaComum;

        public PaginaMaster()
        {
            _paginaComum = new PaginaComum(this);

            _paginaComum.EVENTOcontexto += _paginaComum_EVENTOcontexto;
        }

        private object _paginaComum_EVENTOcontexto()
        {
            return this;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                inputMenu = (Menu) FindControl("inputMenu");

                buscaUsuario();
                montaMenu();
            }

        }

        protected virtual cf.dados.Entidade buscaEntidade()
        {
            cf.dados.Entidade retorno = null;

            return retorno;
        }

        void buscaUsuario()
        {
            cf.dados.Entidade oEntidade = buscaEntidade();

            ((cf.dados.USUARIO)oEntidade).DS_NOME_USUARIO = _paginaComum.credenciaisNome();
            _paginaComum.carregar(oEntidade);
        }

        void montaMenu2()
        {
            // carrega o xml dos menus

            cf.util.Biblioteca biblioteca = new cf.util.Biblioteca();
            string xmlMenu = biblioteca.obtemRecursoTexto("CUTIL.RECURSOS.XmlMenu.xml");


            System.Xml.XmlDocument xmlDocumentMenu = new System.Xml.XmlDocument();
            xmlDocumentMenu.LoadXml(xmlMenu);

            // adiciona o item de menu

            int i = -1;

            foreach (System.Xml.XmlNode xmlNodePai in xmlDocumentMenu.SelectNodes("menus/menu"))
            {
                i++;
      

                MenuItem oMenuItem = new MenuItem();

                string menuText = xmlNodePai.SelectSingleNode("@nome").InnerText;
                oMenuItem.Text = menuText;
                oMenuItem.Value = i.ToString();

                inputMenu.Items.Add(oMenuItem);

                foreach (System.Xml.XmlNode xmlNode in xmlNodePai.SelectNodes("pagina"))
                {
                    MenuItem oMenuItemFilho = new MenuItem();
                    

                    string menuUrl = xmlNode.SelectSingleNode("@url").InnerText;
                    menuText = xmlNode.SelectSingleNode("@nome").InnerText;
                    oMenuItemFilho.NavigateUrl = menuUrl;
                    oMenuItemFilho.Text = menuText;
                    oMenuItemFilho.Value = "0";

                    inputMenu.Items[i].ChildItems.Add(oMenuItemFilho);





                }

            }





        }
        void montaMenu()
        {

#if DEBUG
             montaMenu2();
                        return;
#endif

            cf.dados.MENU oEntidade = new cf.dados.MENU();

            cf.dados.MENU oEntidade1 = oEntidade;

            oEntidade.pesquisar();

            inputMenu.Visible = false;

            foreach (cf.dados.MENU oItem in oEntidade)
            {
                montaMenuItemAdiciona(oItem);
            }

            //

            //oEntidade = new cf.dados.MENU();
            //oEntidade.pesquisar();
            //((IEnumerator)oEntidade).Reset();


            oEntidade = new cf.dados.MENU();
            oEntidade.pesquisarFilhos();


            foreach (cf.dados.MENU oItem in oEntidade)
            {
                montaMenuItemHierarquia(oItem);
            }

            inputMenu.Visible = true;

        }

        void montaMenuItemAdiciona(cf.dados.MENU oItem)
        {
            MenuItem oMenuItem = new MenuItem();
            

            oMenuItem.NavigateUrl = oItem.pagina.DS_URL;
            oMenuItem.Text = oItem.DS_NOME_MENU;
            oMenuItem.Value = oItem.codigo.ToString();

            inputMenu.Items.Add(oMenuItem);
        }

        void montaMenuItemHierarquia(cf.dados.MENU oItem)
        {
            if (oItem.menupai != null && oItem.menupai.codigo != 0) // tem pai
            {
                MenuItem oMenuItem = inputMenu.FindItem(oItem.codigo.ToString());

                MenuItem menuItemPai = inputMenu.FindItem(oItem.menupai.codigo.ToString());

                if (oMenuItem != null && menuItemPai != null)
                {
                    inputMenu.Items.Remove(oMenuItem);

                    menuItemPai.ChildItems.Add(oMenuItem);


                }
            }
        }

        protected void input_Click(object sender, EventArgs e)
        {
            _paginaComum.entidadeNome = buscaEntidade().ToString().ToLower();
            _paginaComum.entidade = buscaEntidade();

            _paginaComum.input_Click(sender, e);
        }
    }

    public class PaginaComum
    {
        Page _pagina;

        MasterPage _paginaMaster;

        cf.util.Biblioteca _biblioteca;

        string sMaster = "ContentPlaceHolder1";

        string _entidadeNome;

        HttpRequest oHttpRequest;
        HttpResponse oHttpResponse;

        cf.dados.Entidade _entidade;
        cf.dados.EntidadeBase _entidadeBase;

        DataGrid _grade;

        public DataGrid grade
        {
            get { return _grade; }
            set { _grade = value; }
        }

        public string entidadeNome
        {
            get { return _entidadeNome; }
            set { _entidadeNome = value; }
        }

        public cf.dados.Entidade entidade
        {
            get { return _entidade; }
            set { _entidade = value; }
        }

        public cf.dados.EntidadeBase entidadeBase
        {
            get { return _entidadeBase; }
            set { _entidadeBase = value; }
        }

        public PaginaComum(Page pagina)
        {
            _pagina = pagina;

            _biblioteca = new Biblioteca();

            //oHttpRequest = _pagina.Request;

            //oHttpResponse = _pagina.Response;
        }

        public PaginaComum(MasterPage master)
        {
            _paginaMaster = master;

            _biblioteca = new Biblioteca();

            //oHttpRequest = _paginaMaster.Request;

            //oHttpResponse = _paginaMaster.Response;
        }

        public delegate object EVcontexto();

        public event EVcontexto EVENTOcontexto;

        void contexto()
        {
            if (EVENTOcontexto != null)
            {
                object oPagina = EVENTOcontexto();
                if (oPagina is Page) { _pagina = (Page)oPagina; }
                if (oPagina is MasterPage) { _paginaMaster = (MasterPage)oPagina; }

                if (_pagina != null)
                {
                    oHttpRequest = _pagina.Request;

                    oHttpResponse = _pagina.Response;
                }
                else if (_paginaMaster != null)
                {
                    oHttpRequest = _paginaMaster.Request;

                    oHttpResponse = _paginaMaster.Response;
                }
            }



        }
        
        public string navegar(string entidade, string de, string para)
        {
            cf.util.Biblioteca oLib = new cf.util.Biblioteca();

            string sXML = oLib.obtemRecursoTexto("CUTIL.XML.navegacao.xml");


            System.Xml.XmlDocument oXmlDocument = new System.Xml.XmlDocument();
            oXmlDocument.LoadXml(sXML);

            string sUrl = null;

            // System.Xml.XmlNode oXmlNode = oXmlDocument.SelectSingleNode("/navegacao/entidade [@nome='usuario']/acao [@nome='listar']/acao [@nome='voltar']/@url");
            System.Xml.XmlNode oXmlNode = oXmlDocument.SelectSingleNode("/navegacao/entidade [@nome='" + entidade + "']/acao [@nome='" + de + "']/acao [@nome='" + para + "']/@url");
            sUrl = oXmlNode.Value;

            return sUrl;
        }

        public void credenciaisVerificar()
        {
#if DEBUG
            return;
#endif

            string sUrl = _biblioteca.configLer("login");

            if (_pagina.Page.AppRelativeVirtualPath == sUrl)
            {
                return;
            }

            HttpCookie oHttpCookie = _pagina.Request.Cookies["mae.joana"];
            if (oHttpCookie == null)
            {
                _pagina.Response.Redirect(sUrl);
            }
            if (oHttpCookie.Expires != null && oHttpCookie.Expires > DateTime.Now)
            {
                _pagina.Response.Redirect(sUrl);
            }

        }

        public void credenciaisSalvar(string usuarioNome)
        {
            biscoitoSalvar("usuario.nome", usuarioNome);
        }

        void biscoitoSalvar(string chave, string  valor)
        {
            HttpCookie oHttpCookie = new HttpCookie("mae.joana");

            if (_pagina != null)
            {
                if (_pagina.Request.Cookies["mae.joana"] == null)
                {
                    oHttpCookie.Secure = false;
                    oHttpCookie.Expires = DateTime.Now.AddMinutes(15);
                    oHttpCookie.Values.Add(chave, valor);

                    _pagina.Response.Cookies.Add(oHttpCookie);
                }
                else
                {
                    oHttpCookie = _pagina.Request.Cookies["mae.joana"];
                    oHttpCookie.Secure = false;
                    oHttpCookie.Expires = DateTime.Now.AddMinutes(15);

                    if (oHttpCookie.Values[chave] != null)
                    {
                        oHttpCookie.Values[chave] = valor;
                    }
                    else
                    {
                        oHttpCookie.Values.Add(chave, valor);
                    }

                    _pagina.Response.SetCookie(oHttpCookie);
                }
            }
            else if (_paginaMaster != null)
            {
                if (_paginaMaster.Request.Cookies["mae.joana"] == null)
                {
                    oHttpCookie.Secure = false;
                    oHttpCookie.Expires = DateTime.Now.AddMinutes(15);
                    oHttpCookie.Values.Add(chave, valor);

                    _paginaMaster.Response.Cookies.Add(oHttpCookie);
                }
                else
                {
                    oHttpCookie = _paginaMaster.Request.Cookies["mae.joana"];
                    oHttpCookie.Secure = false;
                    oHttpCookie.Expires = DateTime.Now.AddMinutes(15);

                    if (oHttpCookie.Values[chave] != null)
                    {
                        oHttpCookie.Values[chave] = valor;
                    }
                    else
                    {
                        oHttpCookie.Values.Add(chave, valor);
                    }

                    _paginaMaster.Response.SetCookie(oHttpCookie);
                }
            }


        }

        string biscoitoObter(string chave)
        {
            contexto();

            string sRetorno = null;

            if (_pagina != null)
            {
                if (_pagina.Request.Cookies.Count == 0) { return sRetorno; }

                HttpCookie oHttpCookie = _pagina.Request.Cookies["mae.joana"];

                if (oHttpCookie != null)
                {
                    sRetorno = oHttpCookie.Values[chave];
                }
            }
            else if (_paginaMaster != null)
            {
                if (_paginaMaster.Request.Cookies.Count == 0) { return sRetorno; }

                HttpCookie oHttpCookie = _paginaMaster.Request.Cookies["mae.joana"];

                if (oHttpCookie != null)
                {
                    sRetorno = oHttpCookie.Values[chave];
                }
            }



            return sRetorno;
        }

        public void credenciaisExcluir()
        {
            contexto();

            string sUrl = _biblioteca.configLer("login");

            HttpCookie oHttpCookie;

            oHttpCookie = oHttpRequest.Cookies["mae.joana"];

            if (oHttpCookie != null)
            {
                oHttpRequest.Cookies.Remove("mae.joana");
                oHttpResponse.Redirect(sUrl);
            }
        }

        public string credenciaisNome()
        {
            string sRetorno = "Usuário não autenticado";
            HttpCookie oHttpCookie;

            if (_pagina != null)
            {
                oHttpCookie = _pagina.Request.Cookies["mae.joana"];
            }
            else if (_paginaMaster != null)
            {
                oHttpCookie = _paginaMaster.Request.Cookies["mae.joana"];
            }
            else
            {
                throw new Exception("Informe a página ou a página master.");
            }

            if (oHttpCookie != null)
            {
                sRetorno = oHttpCookie["usuario.nome"];
            }
            return sRetorno;
        }

        public void credenciaisLinguagemSelecionar(string iso)
        {
            biscoitoSalvar("linguagem", iso);
        }

        public string credenciaisLinguagemObter()
        {
            return biscoitoObter("linguagem");
        }

        public void carregar(cf.dados.Entidade entidade)  // copia os valores da entidade para interface
        {
            carregar(entidade, 0);
        }

        public void carregar(cf.dados.Entidade entidade, int nivel)  // copia os valores da entidade para interface
        {
            if (nivel > 1) { return; }
            // busca as propriedades

            ATabelaColuna oATabelaColuna = null;

            foreach (System.Reflection.PropertyInfo oPropertyInfo in entidade.GetType().GetProperties())
            {
                object[] oAtributosColuna = oPropertyInfo.GetCustomAttributes(typeof(ATabelaColuna), true);

                bool bTabelaColuna = false;
                foreach (object obj in oAtributosColuna)
                {
                    bTabelaColuna = (obj is ATabelaColuna);
                    if (bTabelaColuna) { oATabelaColuna = (ATabelaColuna)obj; break; }
                }

                if (bTabelaColuna)
                {

                    object oValor = oPropertyInfo.GetValue(entidade);
                    string sID = entidade.GetType().Name + "_" + oPropertyInfo.Name; sID = sID.ToUpper();

                    object oObject;

                    if (_pagina != null)
                    {
                        oObject = _pagina.Master.FindControl(sMaster).FindControl(sID);
                    }
                    else if (_paginaMaster != null)
                    {
                        oObject = _paginaMaster.FindControl(sID);
                    }
                    else
                    {
                        throw new Exception("Informe a página ou a página master.");
                    }


                    if (oObject != null && oValor != null)
                    {
                        if (oObject is WebControl)
                        {
                            WebControl oControl = (WebControl)oObject;

                            string sNomeTipo = oControl.GetType().Name;
                            if (sNomeTipo == "TextBox")
                            {
                                ((TextBox)oControl).Text = oValor.ToString();
                            }
                            else if (sNomeTipo == "Label")
                            {
                                ((Label)oControl).Text = oValor.ToString();
                            }
                            else if (sNomeTipo == "FileUpload")
                            {
                                // não deve carregar nada, no caso de stream será sempre do sentido  camada ui para camada de dados 
                            }

                        }

                        if (oObject is HiddenField)
                        {
                            HiddenField oControl = (HiddenField)oObject;
                            oControl.Value = oValor.ToString();
                        }
                    }
                }
                else // pode ser página
                {
                    ATabela oATabela = null;

                    object[] oAtributosTabela = oPropertyInfo.GetCustomAttributes(typeof(ATabela), true);

                    bool bTabela = false;
                    foreach (object obj in oAtributosTabela)
                    {
                        bTabela = (obj is ATabela);
                        if (bTabela) { oATabela = (ATabela)obj; break; }
                    }

                    if (bTabela)
                    {

                        object oValor = oPropertyInfo.GetValue(entidade);
                        cf.dados.Entidade oEntidade1 = (cf.dados.Entidade)oValor;

                        // busca as propriedades

                        oATabelaColuna = null;

                        foreach (System.Reflection.PropertyInfo oPropertyInfo1 in oEntidade1.GetType().GetProperties())
                        {
                            oAtributosColuna = oPropertyInfo1.GetCustomAttributes(typeof(ATabelaColuna), true);

                            bTabelaColuna = false;
                            foreach (object obj in oAtributosColuna)
                            {
                                bTabelaColuna = (obj is ATabelaColuna);
                                if (bTabelaColuna) { oATabelaColuna = (ATabelaColuna)obj; break; }
                            }

                            if (bTabelaColuna)
                            {

                                oValor = oPropertyInfo1.GetValue(oEntidade1);
                                string sID = oEntidade1.GetType().Name + "_" + oPropertyInfo1.Name; sID = sID.ToUpper();

                                //object oObject = this.Master.FindControl(sMaster).FindControl(sID);

                                object oObject;

                                if (_pagina != null)
                                {
                                    oObject = _pagina.Master.FindControl(sMaster).FindControl(sID);
                                }
                                else if (_paginaMaster != null)
                                {
                                    oObject = _paginaMaster.FindControl(sID);
                                }
                                else
                                {
                                    throw new Exception("Informe a página ou a página master.");
                                }

                                if (oObject != null && oValor != null)
                                {
                                    if (oObject is WebControl)
                                    {
                                        WebControl oControl = (WebControl)oObject;

                                        string sNomeTipo = oControl.GetType().Name;
                                        if (sNomeTipo == "TextBox")
                                        {
                                            ((TextBox)oControl).Text = oValor.ToString();
                                        }

                                    }

                                    if (oObject is HiddenField)
                                    {
                                        HiddenField oControl = (HiddenField)oObject;                                        oControl.Value = oValor.ToString();

                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        internal void descarrega(cf.dados.Entidade entidade) // copia os valores  da interface para a entidade
        {

            // busca as propriedades

            ATabelaColuna oATabelaColuna = null;

            foreach (System.Reflection.PropertyInfo oPropertyInfo in entidade.GetType().GetProperties())
            {
                object[] oAtributosColuna = oPropertyInfo.GetCustomAttributes(typeof(ATabelaColuna), true);

                bool bTabelaColuna = false;
                foreach (object obj in oAtributosColuna)
                {
                    bTabelaColuna = (obj is ATabelaColuna);
                    if (bTabelaColuna) { oATabelaColuna = (ATabelaColuna)obj; break; }
                }

                if (bTabelaColuna)
                {
                    object sValor = null;

                    string sID = entidade.GetType().Name + "_" + oPropertyInfo.Name; sID = sID.ToUpper();

                    //object oObject = this.Master.FindControl(sMaster).FindControl(sID);

                    object oObject;

                    if (_pagina != null)
                    {
                        oObject = _pagina.Master.FindControl(sMaster).FindControl(sID);
                    }
                    else if (_paginaMaster != null)
                    {
                        oObject = _paginaMaster.FindControl(sID);
                    }
                    else
                    {
                        throw new Exception("Informe a página ou a página master.");
                    }

                    if (oObject != null)
                    {
                        bool bAtribuir = false; // Valor da interface não é nulo, então atribuir 

                        if (oObject is WebControl)
                        {
                            WebControl oControl = (WebControl)oObject;

                            string sNomeTipo = oControl.GetType().Name;

                            if (sNomeTipo == "TextBox")
                            {
                                sValor = ((TextBox)oControl).Text;
                                bAtribuir = sValor.ToString().Length > 0;
                            }


                            if (sNomeTipo == "FileUpload")
                            {
                                sValor = ((FileUpload)oControl).FileContent;
                                bAtribuir = !(sValor == null);
                            }

                        }

                        if (oObject is HiddenField)
                        {
                            HiddenField oControl = (HiddenField)oObject;
                            sValor = oControl.Value;

                            bAtribuir = sValor.ToString().Length > 0;
                        }

                        if (bAtribuir)
                        {
                            if (oPropertyInfo.PropertyType.Name == "DateTime") { sValor = DateTime.Parse(sValor.ToString()); }
                            if (oPropertyInfo.PropertyType.Name == "Int64") { sValor = Int64.Parse("0" + sValor.ToString()); }

                            oPropertyInfo.SetValue(entidade, sValor);

                        }
                    }
                }
            }

            // busca os relacionamentos


            ATabela oATabela = null;

            foreach (System.Reflection.PropertyInfo oPropertyInfo in entidade.GetType().GetProperties())
            {
                object[] oAtributosTabela = oPropertyInfo.GetCustomAttributes(typeof(ATabela), true);

                bool bTabela = false;
                foreach (object obj in oAtributosTabela)
                {
                    bTabela = (obj is ATabela);
                    if (bTabela) { oATabela = (ATabela)obj; break; }
                }

                if (bTabela)
                {

                    object oValor = oPropertyInfo.GetValue(entidade);

                    this.descarrega((Entidade)oValor); // entidade

                }
            }
        }

        public void transportaEntity(cf.dados.EntidadeBase entidade, cf.comum.enumeracoes.CFSENTIDO sentido) // ui2obj
        {
            
            cf.dados.EntidadeBase[] entidades = { };

            entidades = entidade.buscaEntidades();

            foreach (cf.dados.EntidadeBase entidadeBase in entidades)
            {
                if (entidadeBase == null) { continue; }
                
                foreach (System.Reflection.PropertyInfo oPropertyInfo in entidadeBase.GetType().GetProperties())  
                {
                    object oObject;

                    string sID = entidadeBase.GetType().Name + "_" + oPropertyInfo.Name; sID = sID.ToUpper();
                    //sID = entidadeBase.GetType().BaseType.Name + "_" + oPropertyInfo.Name; sID = sID.ToUpper();

                    if (_pagina != null)
                    {
                        oObject = _pagina.Master.FindControl(sMaster).FindControl(sID);
                    }
                    else if (_paginaMaster != null)
                    {
                        oObject = _paginaMaster.FindControl(sID);
                    }
                    else
                    {
                        throw new Exception("Informe a página ou a página master.");
                    }



                    if (sentido == cf.comum.enumeracoes.CFSENTIDO.obj2ui)
                    {


                        if (oObject != null) // && oValor != null
                        {
                            object oValor = oPropertyInfo.GetValue(entidadeBase);

                            if (oValor != null)
                            {
                                if (oObject is WebControl)
                                {
                                    WebControl oControl = (WebControl)oObject;

                                    string sNomeTipo = oControl.GetType().Name;
                                    if (sNomeTipo == "TextBox")
                                    {
                                        ((TextBox)oControl).Text = "" + oValor.ToString();
                                    }
                                    else if (sNomeTipo == "Label")
                                    {
                                        ((Label)oControl).Text = oValor.ToString();
                                    }
                                    else if (sNomeTipo == "FileUpload")
                                    {
                                        // não deve carregar nada, no caso de stream será sempre do sentido  camada ui para camada de dados 
                                    }

                                }

                                if (oObject is HiddenField)
                                {
                                    HiddenField oControl = (HiddenField)oObject;
                                    oControl.Value = oValor.ToString();
                                }
                            }


                        }

                    }
                    else
                    {
                        object oValor = null;

                        if (oObject != null) //  && oValor != null
                        {
                            if (oObject is WebControl)
                            {
                                WebControl oControl = (WebControl)oObject;

                                string sNomeTipo = oControl.GetType().Name;
                                if (sNomeTipo == "TextBox")
                                {
                                    oValor = ((TextBox)oControl).Text;// = oValor.ToString();
                                }
                                else if (sNomeTipo == "Label")
                                {
                                    oValor = ((Label)oControl).Text;// = oValor.ToString();
                                }
                                else if (sNomeTipo == "FileUpload")
                                {
                                    // não deve carregar nada, no caso de stream será sempre do sentido  camada ui para camada de dados 
                                }

                            }

                            if (oObject is HiddenField)
                            {
                                HiddenField oControl = (HiddenField)oObject;
                                oValor = oControl.Value;// = oValor.ToString();
                            }

                            if (oValor != null && oValor.ToString().Length > 0)
                            {
                                if (oPropertyInfo.PropertyType.Name == "Int64")
                                {
                                    oValor = Int64.Parse("0"+oValor.ToString());
                                }
                                else if (oPropertyInfo.PropertyType.Name == "DateTime")
                                {
                                    oValor = DateTime.Parse(oValor.ToString());
                                }
                                else if (oPropertyInfo.PropertyType.Name == "Decimal")
                                {
                                    oValor = decimal.Parse("0" + oValor.ToString());
                                }


                                oPropertyInfo.SetValue(entidadeBase, oValor);

                                if (entidade.GetType().GetProperty(oPropertyInfo.ReflectedType.Name.ToLower()) != null)
                                {
                                    entidade.GetType().GetProperty(oPropertyInfo.ReflectedType.Name.ToLower()).SetValue(entidade, entidadeBase);
                                }

                            }

                        }
                    }



                }

            }

            //foreach (System.Reflection.PropertyInfo oPropertyInfo in entidade.GetType().GetProperties())
            //{
            //    object oValor = oPropertyInfo.GetValue(entidade);

            //    Boolean isBase = (oValor is EntidadeBase);
            //    if (isBase)
            //    {
            //        foreach (cf.dados.EntidadeBase entidadeBase in entidades)
            //        {
            //            if (oValor.GetType().Equals(entidadeBase.GetType()) )
            //            {
            //                oValor = entidadeBase;

            //                oPropertyInfo.SetValue(entidade, oValor);
            //            }
            //        }
            //    }
            //}

        }

        internal void descarregaEntity(cf.dados.EntidadeBase entidade) // copia os valores  da interface para a entidade
        {
            transportaEntity(entidade, cf.comum.enumeracoes.CFSENTIDO.ui2obj); return;

            // busca as propriedades

            //ATabelaColuna oATabelaColuna = null;

            foreach (System.Reflection.PropertyInfo oPropertyInfo in entidade.GetType().GetProperties())
            {
                //object[] oAtributosColuna = oPropertyInfo.GetCustomAttributes(typeof(ATabelaColuna), true);
                //object[] oAtributosColuna = oPropertyInfo.GetCustomAttributes(true);

                //bool bTabelaColuna = false;
                //foreach (object obj in oAtributosColuna)
                //{
                //    bTabelaColuna = (obj is ATabelaColuna) || (!(obj is System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute));
                //    if (bTabelaColuna) { oATabelaColuna = (ATabelaColuna)obj; break; }
                //    if (bTabelaColuna) { break; }
                //}

                if (oPropertyInfo.PropertyType.BaseType != null && oPropertyInfo.PropertyType.BaseType.Name != "EntidadeBase")//              (!(oPropertyInfo.GetValue(entidade) is  EntidadeBase)) //  bTabelaColuna
                {
                    object sValor = null;

                    string sID = entidade.GetType().Name + "_" + oPropertyInfo.Name; sID = sID.ToUpper();

                    //object oObject = this.Master.FindControl(sMaster).FindControl(sID);

                    object oObject;

                    if (_pagina != null)
                    {
                        oObject = _pagina.Master.FindControl(sMaster).FindControl(sID);
                    }
                    else if (_paginaMaster != null)
                    {
                        oObject = _paginaMaster.FindControl(sID);
                    }
                    else
                    {
                        throw new Exception("Informe a página ou a página master.");
                    }

                    if (oObject != null)
                    {
                        bool bAtribuir = false; // Valor da interface não é nulo, então atribuir 

                        if (oObject is WebControl)
                        {
                            WebControl oControl = (WebControl)oObject;

                            string sNomeTipo = oControl.GetType().Name;

                            if (sNomeTipo == "TextBox")
                            {
                                sValor = ((TextBox)oControl).Text;
                                bAtribuir = sValor.ToString().Length > 0;
                            }


                            if (sNomeTipo == "FileUpload")
                            {
                                sValor = ((FileUpload)oControl).FileContent;
                                bAtribuir = !(sValor == null);
                            }

                        }

                        if (oObject is HiddenField)
                        {
                            HiddenField oControl = (HiddenField)oObject;
                            sValor = oControl.Value;

                            bAtribuir = sValor.ToString().Length > 0;
                        }


                        if (bAtribuir)
                        {
                            if (oPropertyInfo.PropertyType.Name == "DateTime") { sValor = DateTime.Parse(sValor.ToString()); }
                            if (oPropertyInfo.PropertyType.Name == "Int64") { sValor = Int64.Parse("0" + sValor.ToString()); }

                            oPropertyInfo.SetValue(entidade, sValor);

                        }
                    }
                }
                else // if (oObject is EntidadeBase)
                {

                    if (oPropertyInfo.PropertyType.FullName.IndexOf("Collection") >= 0 ){ continue; }

                    cf.dados.EntidadeBase entidadeRelacionada = (EntidadeBase) oPropertyInfo.GetValue(entidade);

                    if(entidadeRelacionada == null){ continue; }
                 
                    foreach (System.Reflection.PropertyInfo oPropertyInfoRelacionada in entidadeRelacionada.GetType().GetProperties())
                    {
                        object[] oAtributosColuna = oPropertyInfoRelacionada.GetCustomAttributes(true); // typeof(ATabelaColuna),
                        foreach (object obj in oAtributosColuna)
                        {
                            if (obj is System.ComponentModel.DataAnnotations.KeyAttribute)
                            {
                                object sValor = null;

                                string sIDR = entidadeRelacionada.GetType().Name + "_" + oPropertyInfoRelacionada.Name; sIDR = sIDR.ToUpper();
                                sIDR = entidadeRelacionada.GetType().Name + "_CODIGO"; sIDR = sIDR.ToUpper();

                                //object oObject = this.Master.FindControl(sMaster).FindControl(sID);

                                object oObjectR;

                                if (_pagina != null)
                                {
                                    oObjectR = _pagina.Master.FindControl(sMaster).FindControl(sIDR);
                                }
                                else if (_paginaMaster != null)
                                {
                                    oObjectR = _paginaMaster.FindControl(sIDR);
                                }
                                else
                                {
                                    throw new Exception("Informe a página ou a página master.");
                                }

                                bool bAtribuirR = false;

                                if (oObjectR != null)
                                {
                                    if (oObjectR is HiddenField)
                                    {
                                        HiddenField oControl = (HiddenField)oObjectR;
                                        sValor = oControl.Value;

                                        bAtribuirR = sValor.ToString().Length > 0;
                                    }
                                }

                                if (bAtribuirR)
                                {
                                    if (oPropertyInfoRelacionada.PropertyType.Name == "DateTime") { sValor = DateTime.Parse(sValor.ToString()); }
                                    if (oPropertyInfoRelacionada.PropertyType.Name == "Int64") { sValor = Int64.Parse("0" + sValor.ToString()); }

                                    decimal nCodigo = Decimal.Parse(sValor.ToString());
                                    oPropertyInfoRelacionada.SetValue(entidadeRelacionada, nCodigo);
                                    entidadeRelacionada.codigo = (long) nCodigo;

                                }

                            }


                            if (obj is System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute) { break; }
                        }

                    }


                }
            }

            // busca os relacionamentos

            ATabela oATabela = null;

            foreach (System.Reflection.PropertyInfo oPropertyInfo in entidade.GetType().GetProperties())
            {
                object[] oAtributosTabela = oPropertyInfo.GetCustomAttributes(typeof(ATabela), true);

                bool bTabela = false;
                foreach (object obj in oAtributosTabela)
                {
                    bTabela = (obj is ATabela);
                    if (bTabela) { oATabela = (ATabela)obj; break; }
                }

                if (bTabela)
                {

                    object oValor = oPropertyInfo.GetValue(entidade);

                    this.descarrega((Entidade)oValor); // entidade

                }
            }
        }

        public void carregarEntity(cf.dados.EntidadeBase entidade)  // copia os valores da entidade para interface
        {
            transportaEntity(entidade, cf.comum.enumeracoes.CFSENTIDO.obj2ui); return;

            
            //carregarEntity(entidade, 0);

            foreach (object o in entidade.lista)
            {
                entidade = (EntidadeBase) o;
            }

            foreach (System.Reflection.PropertyInfo oPropertyInfo in entidade.GetType().GetProperties()) // 
            {
                object[] oAtributosColuna = oPropertyInfo.GetCustomAttributes(typeof(ATabelaColuna), true);
                foreach (object obj in oAtributosColuna)
                {
                    if (obj is System.ComponentModel.DataAnnotations.Schema.NotMappedAttribute) {break;}
                }

                if (!oPropertyInfo.PropertyType.IsConstructedGenericType) // cf.dados.EntidadeBase
                {
                    object oValor = oPropertyInfo.GetValue(entidade);
                    string sID = entidade.GetType().Name + "_" + oPropertyInfo.Name; sID = sID.ToUpper();
                    sID = entidade.GetType().BaseType.Name + "_" + oPropertyInfo.Name; sID = sID.ToUpper();
                    object oObject;

                    if (_pagina != null)
                    {
                        oObject = _pagina.Master.FindControl(sMaster).FindControl(sID);
                    }
                    else if (_paginaMaster != null)
                    {
                        oObject = _paginaMaster.FindControl(sID);
                    }
                    else
                    {
                        throw new Exception("Informe a página ou a página master.");
                    }

                    if (oObject != null && oValor != null)
                    {
                        if (oObject is WebControl)
                        {
                            WebControl oControl = (WebControl)oObject;

                            string sNomeTipo = oControl.GetType().Name;
                            if (sNomeTipo == "TextBox")
                            {
                                ((TextBox)oControl).Text = oValor.ToString();
                            }
                            else if (sNomeTipo == "Label")
                            {
                                ((Label)oControl).Text = oValor.ToString();
                            }
                            else if (sNomeTipo == "FileUpload")
                            {
                                // não deve carregar nada, no caso de stream será sempre do sentido  camada ui para camada de dados 
                            }

                        }

                        if (oObject is HiddenField)
                        {
                            HiddenField oControl = (HiddenField)oObject;
                            oControl.Value = oValor.ToString();
                        }
                    }

                }



            }

        }

        public void carregarEntity(cf.dados.EntidadeBase entidade, int nivel)  // copia os valores da entidade para interface
        {
            if (nivel > 1) { return; }
            // busca as propriedades

            ATabelaColuna oATabelaColuna = null;

            foreach (System.Reflection.PropertyInfo oPropertyInfo in entidade.GetType().GetProperties())
            {
                object[] oAtributosColuna = oPropertyInfo.GetCustomAttributes(typeof(ATabelaColuna), true);

                bool bTabelaColuna = false;
                foreach (object obj in oAtributosColuna)
                {
                    bTabelaColuna = (obj is ATabelaColuna);
                    if (bTabelaColuna) { oATabelaColuna = (ATabelaColuna)obj; break; }
                }

                if (bTabelaColuna)
                {

                    object oValor = oPropertyInfo.GetValue(entidade);
                    string sID = entidade.GetType().Name + "_" + oPropertyInfo.Name; sID = sID.ToUpper();

                    object oObject;

                    if (_pagina != null)
                    {
                        oObject = _pagina.Master.FindControl(sMaster).FindControl(sID);
                    }
                    else if (_paginaMaster != null)
                    {
                        oObject = _paginaMaster.FindControl(sID);
                    }
                    else
                    {
                        throw new Exception("Informe a página ou a página master.");
                    }

                    if (oObject != null && oValor != null)
                    {
                        if (oObject is WebControl)
                        {
                            WebControl oControl = (WebControl)oObject;

                            string sNomeTipo = oControl.GetType().Name;
                            if (sNomeTipo == "TextBox")
                            {
                                ((TextBox)oControl).Text = oValor.ToString();
                            }
                            else if (sNomeTipo == "Label")
                            {
                                ((Label)oControl).Text = oValor.ToString();
                            }
                            else if (sNomeTipo == "FileUpload")
                            {
                                // não deve carregar nada, no caso de stream será sempre do sentido  camada ui para camada de dados 
                            }

                        }

                        if (oObject is HiddenField)
                        {
                            HiddenField oControl = (HiddenField)oObject;
                            oControl.Value = oValor.ToString();
                        }
                    }
                }
                else // pode ser página
                {
                    ATabela oATabela = null;

                    object[] oAtributosTabela = oPropertyInfo.GetCustomAttributes(typeof(ATabela), true);

                    bool bTabela = false;
                    foreach (object obj in oAtributosTabela)
                    {
                        bTabela = (obj is ATabela);
                        if (bTabela) { oATabela = (ATabela)obj; break; }
                    }

                    if (bTabela)
                    {

                        object oValor = oPropertyInfo.GetValue(entidade);
                        cf.dados.Entidade oEntidade1 = (cf.dados.Entidade)oValor;

                        // busca as propriedades

                        oATabelaColuna = null;

                        foreach (System.Reflection.PropertyInfo oPropertyInfo1 in oEntidade1.GetType().GetProperties())
                        {
                            oAtributosColuna = oPropertyInfo1.GetCustomAttributes(typeof(ATabelaColuna), true);

                            bTabelaColuna = false;
                            foreach (object obj in oAtributosColuna)
                            {
                                bTabelaColuna = (obj is ATabelaColuna);
                                if (bTabelaColuna) { oATabelaColuna = (ATabelaColuna)obj; break; }
                            }

                            if (bTabelaColuna)
                            {

                                oValor = oPropertyInfo1.GetValue(oEntidade1);
                                string sID = oEntidade1.GetType().Name + "_" + oPropertyInfo1.Name; sID = sID.ToUpper();

                                //object oObject = this.Master.FindControl(sMaster).FindControl(sID);

                                object oObject;

                                if (_pagina != null)
                                {
                                    oObject = _pagina.Master.FindControl(sMaster).FindControl(sID);
                                }
                                else if (_paginaMaster != null)
                                {
                                    oObject = _paginaMaster.FindControl(sID);
                                }
                                else
                                {
                                    throw new Exception("Informe a página ou a página master.");
                                }

                                if (oObject != null && oValor != null)
                                {
                                    if (oObject is WebControl)
                                    {
                                        WebControl oControl = (WebControl)oObject;

                                        string sNomeTipo = oControl.GetType().Name;
                                        if (sNomeTipo == "TextBox")
                                        {
                                            ((TextBox)oControl).Text = oValor.ToString();
                                        }

                                    }

                                    if (oObject is HiddenField)
                                    {
                                        HiddenField oControl = (HiddenField)oObject;
                                        oControl.Value = oValor.ToString();
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public void input_Click(object sender, EventArgs e)
        {
            contexto();

            if (sender is Button)
            {
                Button botao = (Button)sender;

                if (botao.ID == "inputIncluir")
                {
                    if (_entidadeNome == null) { throw new Exception("informe PaginaComum.entidadeNome"); }

                    string sEntidade = _entidadeNome;
                    string sUrl = navegar(sEntidade, "listar", "incluir"); // "usuario"

                    oHttpResponse.Redirect(sUrl);
                }
                else if (botao.ID == "inputVoltar")
                {
                    if (_entidadeNome == null) { throw new Exception("informe PaginaComum.entidadeNome"); }

                    string sEntidade = _entidadeNome;
                    string sUrl = navegar(sEntidade, "manter", "voltar"); // "usuario"

                    oHttpResponse.Redirect(sUrl);
                }
                else if (botao.ID == "inputSalvar")
                {
                    // busca a entidade, consulta, copia dados do formulário para  a entidade

                    if (oHttpRequest["codigo"] != null)
                    {
#if ENTITY
                        _entidadeBase.codigo = Int64.Parse(this.oHttpRequest["codigo"]);
                        if (_entidadeBase.codigo != 0) // alterar
                        {
                            _entidadeBase.consultar();
                            if (_entidadeBase.Count > 0)
                            {
                                descarregaEntity(_entidadeBase);
                                _entidadeBase.alterar();

                            }
                        }
                        else // incluir
                        {
                            descarregaEntity(_entidadeBase);
                            _entidadeBase.codigo = 0;
                            _entidadeBase.incluir();
                        }
#else
                        _entidade.codigo = Int64.Parse(this.oHttpRequest["codigo"]);
                        if (_entidade.codigo != 0) // alterar
                        {
                            _entidade.consultar();
                            if (_entidade.Count > 0)
                            {
                                descarrega(_entidade);
                                _entidade.alterar();

                            }
                        }
                        else // incluir
                        {
                            descarrega(_entidade);
                            _entidade.codigo = 0;
                            _entidade.incluir();
                        }
#endif

                    }
                    else // incluir
                    {
                        descarrega(_entidade);
                        _entidade.codigo = 0;
                        _entidade.incluir();
                    }

                    // redireciona

                    if (_entidadeNome == null) { throw new Exception("informe PaginaComum.entidadeNome"); }

                    string sEntidade = _entidadeNome;
                    string sUrl = navegar(sEntidade, "manter", "salvar"); // "usuario"

                    oHttpResponse.Redirect(sUrl);
                }
                else if (botao.ID == "inputExcluir")
                {
                    if (oHttpRequest["codigo"] != null)
                    {
                        _entidade.codigo = Int64.Parse(oHttpRequest["codigo"]);
                        if (_entidade.codigo != 0) // alterar
                        {
                            _entidade.consultar();
                            if (_entidade.Count > 0)
                            {
                                _entidade.excluir();

                            }
                        }

                        // redireciona

                        if (_entidadeNome == null) { throw new Exception("informe PaginaComum.entidadeNome"); }

                        string sEntidade = _entidadeNome;
                        string sUrl = navegar(sEntidade, "manter", "excluir"); // "usuario"
                    }
                }
                else if (botao.ID == "inputEntrar")
                {
                    //descarrega(_entidade);
                    descarregaEntity(_entidadeBase);

                    //((iUsuario)_entidade).autenticar();
                    ((iUsuario)_entidadeBase).autenticar();

                    //if (_entidade.Count == 0)
                    if (_entidadeBase.Count == 0)
                    {
                            throw new Exception("Usuário não cadastrado.");
                    }

                    //string sUsuarioLogin = ((USUARIO)_entidade).DS_NOME_USUARIO;
                    string sUsuarioLogin = ((USUARIO)_entidadeBase).DS_NOME_USUARIO;
                    credenciaisSalvar(sUsuarioLogin);

                    //FormsAuthentication.RedirectFromLoginPage(sUsuarioLogin, true);

                    // redireciona

                    if (_entidadeNome == null) { throw new Exception("informe PaginaComum.entidadeNome"); }

                    string sEntidade = _entidadeNome;
                    string sUrl = navegar(sEntidade, "login", "entrar"); // "usuario"

                    oHttpResponse.Redirect(sUrl);
                }
                else  if (botao.ID == "inputSair")
                {
                    credenciaisExcluir();

                    string sEntidade = "todas.entidades";
                    string sUrl = navegar(sEntidade, "logado", "sair"); // "usuario"

                    oHttpResponse.Redirect(sUrl);
                }
                else if (botao.ID == "inputPesquisar")
                {
#if ENTITY

                    _grade.Visible = true;
                    descarregaEntity(_entidadeBase);
                    _entidadeBase.pesquisar();

                    //_grade.DataSource = _entidadeBase.lista;
                    _grade.DataSource = _entidadeBase;
                    _grade.DataBind();

#else

                _entidade = buscaEntidade();

                _entidade.pesquisar();


                _grade.DataSource = _entidade;
                _grade.DataBind();

#endif
                }
            }
            if (sender is DropDownList)
            {
                DropDownList lista = (DropDownList) sender;
                if (lista.ID == "inputBandeiras")
                {
                    credenciaisLinguagemSelecionar(lista.SelectedValue);
                    oHttpResponse.Redirect( oHttpRequest.Url.ToString() );

                }
            }
        }
    }

}
