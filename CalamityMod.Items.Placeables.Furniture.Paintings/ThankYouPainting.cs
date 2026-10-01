using System.Collections.Generic;
using System.Linq;
using CalamityMod.Tiles.Furniture.Paintings;
using Terraria;
using Terraria.ModLoader;

namespace CalamityMod.Items.Placeables.Furniture.Paintings;

public class ThankYouPainting : ModItem, ILocalizedModType, IModType
{
	public const int DropInt = 100;

	public static IList<string> devList = new List<string>
	{
		"Altixal", "apotofkoolaid", "Atalya", "Ben-TK", "Big E", "carnymassacre", "CDMusic", "CongratsIsTrash", "Cooper", "CosmaticMango",
		"CrabBar", "Dandy", "Dia", "Done", "dozezoze", "ENNWAY", "Flowaria", "Fluffy", "fryzahh", "HaguriHat",
		"jasper", "LordMetarex", "math2", "Moonburn", "Ozzatron", "Piky", "PokerFace", "Raesh", "Sagittariod", "sixtydegrees",
		"StipulateVenus", "Sunny", "_tofu", "Tomat", "Triangle", "TYESKI (Universe)", "Xyk", "YuH", "Afzofa", "AdipemDragon",
		"Akeeli", "Aleksh", "Alphi", "Altalyra", "Amadis", "AquaSG", "AstroKnight", "Blast", "Blastitle", "Blockaroz",
		"Boffin", "Bravioli", "Cei", "Chetto", "Chill Dude", "Cobalion", "Daim", "DarkTiny", "Demik", "DM Dokuro",
		"Doog", "drh", "dwshin", "DylanDoe21", "Earth", "EchoDuck", "Eddie Spaghetti", "Ein", "enamoured", "Enreden",
		"Epsilon", "Fargowilta", "Frous", "Gahtao", "Gamagamer64", "GramOfSalt", "Graydee", "Grox the Great", "Heart Plus Up!", "Hectique",
		"Hugekraken", "Huggles", "Ian-1KV", "IbanPlay", "Inanis", "JaceDaDorito", "Jenosis", "jontchua", "Khaelis", "KnightyKnight",
		"L0st", "Leon", "Leviathan", "Lilac Olligoci", "Lompl Allimath", "Lucille Karma", "MarieArk", "Mercutio 'Merkalto' Takle", "Mihaii", "Minecat",
		"Mishiro Usui", "Mrrp", "Nao", "Neverglide", "Nincity", "Niorin", "Nitro", "Nycro", "NyctoDarkMatter", "PaleoStar",
		"Pbtopacio", "Phantasmal Deathray", "Phupperbat", "Pinkie Poss", "Poly", "Popo", "Poroboros", "President Waluigi", "Puff", "Purple Necromancer",
		"RoverdriveX", "Runefield", "Sargassum", "sentri", "Shade", "SharZz", "Shucks", "Silver-Lord of Ash", "SixteenInMono", "Skeletony",
		"Sok", "Spider Prov", "spooktacular", "Spoopyro", "Svante", "Sylvium", "Teragat", "Terry N. Muse", "ThousandFields", "TikiWiki",
		"Tinymanx", "Trivaxy", "Tobias", "Uberransy", "Uncle Danny", "Vaikyia", "Vladimier", "Yatagarasu", "Yuyutsu", "Zach",
		"Ziggums"
	};

	public new string LocalizationCategory => "Items.Placeables";

	public override void SetDefaults()
	{
		base.Item.DefaultToPlaceableTile(ModContent.TileType<ThankYouPaintingTile>());
		base.Item.width = 96;
		base.Item.height = 64;
		base.Item.value = Item.sellPrice(0, 0, 40);
		base.Item.Calamity().donorItem = true;
	}

	public override void ModifyTooltips(List<TooltipLine> tooltips)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		if (!Main.keyState.PressingShift())
		{
			return;
		}
		string tooltip = "";
		int namesPerLine = 5;
		for (int i = 0; i < devList.Count; i++)
		{
			tooltip += devList[i];
			if (i == devList.Count - 1)
			{
				break;
			}
			tooltip = ((i % namesPerLine != 0) ? (tooltip + ", ") : (tooltip + "\n"));
		}
		TooltipLine line = tooltips.FirstOrDefault((TooltipLine x) => x.Mod == "Terraria" && x.Name == "Tooltip2");
		if (line != null)
		{
			line.Text = tooltip;
		}
	}
}
