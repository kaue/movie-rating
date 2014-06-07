<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Movie.Web.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="headContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainContentPlaceHolder" runat="server">
    <div class="page-wrap">
        <span class="movie-question">Which movie made you feel
            <span style="color: rgb(0, 255, 31)"><%# Emocao.Descricao %></span>
            ?
            <asp:ImageButton runat="server" ID="btnRefresh" CssClass="btn-refresh" ImageUrl="https://cdn1.iconfinder.com/data/icons/TWG_Retina_Icons/64/arrow_circle_left.png" OnClick="btnRefresh_Click" />
        </span>
        <div class="movie-circle" style="float: left;">
            <asp:ImageButton runat="server" ID="imgMovie1Cover" CssClass="movie-image" ImageUrl="http://i.imgur.com/x1pMrKY.png" OnClick="imgMovie1Cover_Click" />
            <asp:Label runat="server" ID="lbMovie1Name" CssClass="movie-name">Movie 1 Name</asp:Label>
        </div>
        <div class="movie-circle" style="float: right;">
            <asp:ImageButton runat="server" ID="imgMovie2Cover" CssClass="movie-image" ImageUrl="http://i.imgur.com/x1pMrKY.png" OnClick="imgMovie2Cover_Click" />
            <asp:Label runat="server" ID="lbMovie2Name" CssClass="movie-name">Movie 2 Name</asp:Label>
        </div>
        <div class="actions">
            <asp:Button runat="server" ID="btnNeither" CssClass="btn btn-action btn-default" Text="Neither" OnClick="txtNeither_Click" />
            <asp:Button runat="server" ID="btnSkip" CssClass="btn btn-action btn-default" Text="Skip Vote" OnClick="btnSkip_Click" />
        </div>
    </div>
    <div class="clearfix"></div>
    <div class="row">
        <span class="movie-question">Movies with most <span style="color: rgb(0, 255, 31)"><%# Emocao.Descricao %></span> votes.</span>
        <asp:Repeater runat="server" ID="rptBestMovies" OnItemDataBound="rptBestMovies_ItemDataBound">
            <ItemTemplate>
                <div class="col-6 col-sm-6 col-lg-4">
                    <div class="card-panel">
                        <div class="front card">
                            <asp:Image runat="server" ID="imgBestMovie" CssClass="movie-image" />
                        </div>
                        <div class="back card">
                            <h4>Emotions</h4>
                            <hr style="margin: -6px 0 0 0; border-top: 2px solid rgba(92, 92, 92, 0.69);">
                            <asp:Repeater runat="server" ID="rptEmotions" OnItemDataBound="rptEmotions_ItemDataBound">
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lbEmotionName" CssClass="emotion-name">Emotion Name</asp:Label>
                                    <div class="prog-bar">
                                        <div class="prog-container">
                                            <asp:Panel runat="server" ID="pnProgressLeft" CssClass="prog-left prog">
                                            </asp:Panel>
                                        </div>
                                        <div class="prog-container">
                                            <asp:Panel runat="server" ID="pnProgressRight" CssClass="prog-right prog">
                                            </asp:Panel>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                    <asp:Label runat="server" ID="lbMovieName" CssClass="movie-name">Movie 2 Name</asp:Label>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        <%--     <div class="clearfix"></div>
        <span class="movie-question">Movies with less
            <span style="color: rgb(0, 255, 31)"><%# Sentimento %></span>
            votes.</span>
        <asp:Repeater runat="server" ID="rptWorstMovies" OnItemDataBound="rptWorstMovies_ItemDataBound">
            <ItemTemplate>
                <div class="col-6 col-sm-6 col-lg-4">
                    <asp:Label runat="server" ID="lbMovieName" CssClass="movie-name">Movie 2 Name</asp:Label>
                    <asp:Image runat="server" ID="imgBestMovie" CssClass="movie-image" />
                </div>
            </ItemTemplate>
        </asp:Repeater>--%>
    </div>
</asp:Content>
