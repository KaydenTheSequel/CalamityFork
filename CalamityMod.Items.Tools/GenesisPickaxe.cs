using CalamityMod.Dusts;
using CalamityMod.Items.Materials;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Items.Tools;

[LegacyName(new string[] { "GallantPickaxe" })]
public class GenesisPickaxe : ModItem, ILocalizedModType, IModType
{
	private int swordDirection;

	public int time;

	public float swingRotation;

	public new string LocalizationCategory => "Items.Tools";

	public override void SetDefaults()
	{
		base.Item.width = 84;
		base.Item.height = 80;
		base.Item.damage = 80;
		base.Item.knockBack = 5.5f;
		base.Item.useTime = 6;
		base.Item.useAnimation = 12;
		base.Item.pick = 225;
		base.Item.tileBoost += 4;
		base.Item.DamageType = DamageClass.Melee;
		base.Item.useTurn = true;
		base.Item.useStyle = 1;
		base.Item.value = CalamityGlobalItem.RarityRedBuyPrice;
		base.Item.rare = 10;
		base.Item.UseSound = SoundID.Item1;
		base.Item.autoReuse = true;
		base.Item.useTurn = true;
	}

	public override void AddRecipes()
	{
		CreateRecipe().AddIngredient<MeldConstruct>(12).AddIngredient(3467, 10).AddTile(412)
			.Register();
	}

	public override void UseAnimation(Player player)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		//IL_000d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0012: Unknown result type (might be due to invalid IL or missing references)
		swordDirection = ((!((player.Center - player.Calamity().mouseWorld).X > 1f)) ? 1 : (-1));
		time = 0;
		swingRotation = 0f;
	}

	public override void MeleeEffects(Player player, Rectangle hitbox)
	{
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_008d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0099: Unknown result type (might be due to invalid IL or missing references)
		//IL_00af: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cb: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0115: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_012e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0135: Unknown result type (might be due to invalid IL or missing references)
		//IL_013f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0144: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_0197: Unknown result type (might be due to invalid IL or missing references)
		//IL_019d: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01db: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_01e2: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fa: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0214: Unknown result type (might be due to invalid IL or missing references)
		//IL_0236: Unknown result type (might be due to invalid IL or missing references)
		//IL_023b: Unknown result type (might be due to invalid IL or missing references)
		player.itemRotation = swingRotation - 1.7f * (float)swordDirection;
		player.itemLocation = player.Center;
		player.direction = swordDirection;
		swingRotation = Utils.Remap(time, 0f, player.itemAnimationMax, 0f, 2.88f * (float)swordDirection);
		Vector2 dustVel2 = Utils.RotatedBy(new Vector2((float)(5 * swordDirection), -5f), (double)(swingRotation - 1.7f * (float)swordDirection), default(Vector2));
		float partScale2 = Main.rand.NextFloat(0.5f, 0.8f);
		Vector2 partVel2 = dustVel2 * Main.rand.NextFloat(0.1f, 0.7f);
		GeneralParticleHandler.SpawnParticle(new HeavySmokeParticle(player.Center + dustVel2 * 12f + Main.rand.NextVector2Circular(8f, 8f), partVel2.RotatedBy(MathHelper.ToRadians(90f * (float)swordDirection)).RotatedBy(-0.3 * (double)swordDirection) * -5f, Color.Black, 13, partScale2, 0.5f, Main.rand.NextFloat(-0.2f, 0.2f)));
		if (Main.rand.NextBool())
		{
			Dust dust = Dust.NewDustPerfect(player.Center + dustVel2 * 12f + Main.rand.NextVector2Circular(8f, 8f), ModContent.DustType<VoidDustInverted>(), partVel2.RotatedBy(MathHelper.ToRadians(90f * (float)swordDirection)).RotatedBy(-0.3 * (double)swordDirection), 0, default(Color), Main.rand.NextFloat(0.9f, 1.25f));
			dust.noGravity = true;
			dust.color = Color.LightGreen;
		}
		time++;
	}
}
