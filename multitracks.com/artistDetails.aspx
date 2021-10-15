<%@ Page Language="C#" AutoEventWireup="true" CodeFile="artistDetails.aspx.cs" Inherits="artistDetails" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
	<link media="all" rel="stylesheet" href="./css/index.css">
	    <link rel="stylesheet" href="https://cdn.jsdelivr.net/npm/bootstrap@5.0.1/dist/css/bootstrap.min.css">

</head>
<body>
    <form id="form1" runat="server">
		<div class="header--mobile--search search">
							<asp:Label Text="Input Artist ID" runat="server" />
							<input type="text" id="txtName" name="Search" value="" />
							<asp:Button Text="Submit" runat="server" OnClick="Submit" />
						
                           <svg class="search--submit--icon">
                              <use xmlns:xlink="http://www.w3.org/1999/xlink" xlink:href="/images/sprite.symbol.svg#Search-Input"></use>
                           </svg>
                        </a>
                     </div>
        <div>
			
            <div class="wrapper mod-standard mod-gray">
				<div class="details-banner">
					<div class="details-banner--overlay"></div>
					<div class="details-banner--hero">
						<asp:Image class="details-banner--hero--img" ImageUrl=<%#Eval("text") %> runat="server" ID="HeaderImage"/>
					</div>
					<div class="details-banner--info">
						<a href="#" class="details-banner--info--box">
								<asp:Image class="details-banner--info--box--img"
								 ImageUrl=<%#Eval("text") %>
								 srcset=<%#Eval("text") %>
							     runat="server" ID="CoverImage"/>
						</a>
						<h1 class="details-banner--info--name"><a class="details-banner--info--name--link" href="#"<asp:Label Text=<%#Eval("text") %> runat="server" ID="ArtistName" /></a></h1>
					</div>
				</div>

				<nav class="discovery--nav">
					<ul class="discovery--nav--list tab-filter--list u-no-scrollbar">
						<li class="discovery--nav--list--item tab-filter--item is-active">
							<a class="tab-filter" href="../artists/details.aspx">Overview</a>
						</li>
						<li class="discovery--nav--list--item tab-filter--item">
							<a class="tab-filter" href="../artists/songs/details.aspx">Songs</a>
						</li>
						<li class="discovery--nav--list--item tab-filter--item">
							<a class="tab-filter" href="../artists/albums/details.aspx">Albums</a>
						</li>
					</ul> <!-- /.browse-header-filters -->
				</nav>

				<div class="discovery--container u-container">
							<main class="discovery--section">

								<section class="standard--holder">
									<div class="discovery--section--header">
										<h2>Top Songs</h2>
										<a class="discovery--section--header--view-all" href="#">View All</a>
									</div><!-- /.discovery-select -->

									<ul id="playlist" class="song-list mod-new mod-menu" runat="server">
										
									</ul><!-- /.song-list -->
								</section><!-- /.songs-section -->

								<div class="discovery--space-saver">
									<section class="standard--holder">
										<div class="discovery--section--header">
											<h2>Albums</h2>
											<a class="discovery--section--header--view-all" href="/artists/default.aspx">View All</a>
										</div><!-- /.discovery-select -->

										<div class="discovery--grid-holder" >
											
											<div class="ly-grid ly-grid-cranberries"  id="albumDiv" runat="server">

												
											</div><!-- /.grid -->
										</div><!-- /.discovery-grid-holder -->
									</section><!-- /.songs-section -->
								</div>

								<section class="standard--holder">
									<div class="discovery--section--header">
										<h2>Biography</h2>
									</div><!-- /.discovery-section-header -->

									<div class="artist-details--biography biography">
										<asp:Label Text=<%#Eval("text") %> runat="server"  ID="Biography"/>
										
										<a href="#">Read More...</a>
									</div>
								</section><!-- /.biography-section -->
							</main><!-- /.discovery-section -->
				</div><!-- /.standard-container -->
			</div>
        </div>
    </form>
</body>
</html>
