using System;
using CalamityMod.Gores.Trees;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.Umbrella;

public class MagicTree : ModProjectile, ILocalizedModType, IModType
{
	private enum Tree
	{
		Astral,
		Corruption,
		Crimson,
		Forest,
		Hallow,
		Jungle,
		Ocean,
		Snow,
		SulphurousSea
	}

	private Tree TreeType = Tree.Forest;

	public static readonly SoundStyle TreeCrashSound = new SoundStyle("CalamityMod/Sounds/Custom/TreeFalling");

	public new string LocalizationCategory => "Projectiles.Summon";

	public override string Texture => "CalamityMod/Projectiles/Summon/Umbrella/TreeForest";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.MinionShot[base.Type] = true;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 300;
		base.Projectile.height = 300;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = DamageClass.Summon;
		base.Projectile.timeLeft = 600;
	}

	public override void AI()
	{
		base.Projectile.rotation += 0.125f * (float)base.Projectile.direction;
		base.Projectile.velocity.Y += 0.3f;
		if (base.Projectile.velocity.Y > 16f)
		{
			base.Projectile.velocity.Y = 16f;
		}
		if (base.Projectile.ai[1] == 0f)
		{
			Array values = Enum.GetValues(typeof(Tree));
			Random random = new Random();
			TreeType = (Tree)values.GetValue(random.Next(values.Length));
			base.Projectile.ai[1]++;
		}
	}

	public override void OnKill(int timeLeft)
	{
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0026: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_005d: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0121: Unknown result type (might be due to invalid IL or missing references)
		//IL_0126: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029d: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a5: Unknown result type (might be due to invalid IL or missing references)
		//IL_02aa: Unknown result type (might be due to invalid IL or missing references)
		//IL_031e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0320: Unknown result type (might be due to invalid IL or missing references)
		//IL_0420: Unknown result type (might be due to invalid IL or missing references)
		//IL_0439: Unknown result type (might be due to invalid IL or missing references)
		//IL_0448: Unknown result type (might be due to invalid IL or missing references)
		//IL_0481: Unknown result type (might be due to invalid IL or missing references)
		//IL_049a: Unknown result type (might be due to invalid IL or missing references)
		//IL_04a9: Unknown result type (might be due to invalid IL or missing references)
		SoundStyle style = TreeCrashSound with
		{
			Volume = 0.5f
		};
		SoundEngine.PlaySound(in style, base.Projectile.Center);
		int creatureAmt = 3;
		for (int t = 0; t < creatureAmt; t++)
		{
			int projType = Utils.SelectRandom<int>(Main.rand, ModContent.ProjectileType<MagicBunny>(), ModContent.ProjectileType<MagicBird>());
			Vector2 velocity = Vector2.Zero;
			if (Main.projectile.IndexInRange((int)base.Projectile.ai[0]))
			{
				velocity = CalamityUtils.GetProjectilePhysicsFiringVelocity(base.Projectile.Center, Main.npc[(int)base.Projectile.ai[0]].Center, 0.28f, 12f);
				velocity.X += Main.rand.NextFloat(-3f, 3f);
			}
			else
			{
				velocity.X = Main.rand.NextFloat(-10f, 10f);
				velocity.Y = Main.rand.NextFloat(-15f, -8f);
			}
			Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, projType, (int)((float)base.Projectile.damage * 0.05f), 0f, base.Projectile.owner);
		}
		int treeGore = 910;
		string TreeTop = "TreeForestTop";
		string TreeBottom = "TreeForestBottom";
		switch (TreeType)
		{
		case Tree.Astral:
			treeGore = ModContent.GoreType<AstralLeaf>();
			TreeTop = "TreeAstralTop";
			TreeBottom = "TreeAstralBottom";
			break;
		case Tree.Corruption:
			treeGore = 915;
			TreeTop = "TreeCorruptionTop";
			TreeBottom = "TreeCorruptionBottom";
			break;
		case Tree.Crimson:
			treeGore = 916;
			TreeTop = "TreeCrimsonTop";
			TreeBottom = "TreeCrimsonBottom";
			break;
		case Tree.Forest:
			treeGore = 910;
			TreeTop = "TreeForestTop";
			TreeBottom = "TreeForestBottom";
			break;
		case Tree.Hallow:
			treeGore = 917;
			TreeTop = "TreeHallowTop";
			TreeBottom = "TreeHallowBottom";
			break;
		case Tree.Jungle:
			treeGore = 914;
			TreeTop = "TreeJungleTop";
			TreeBottom = "TreeJungleBottom";
			break;
		case Tree.Ocean:
			treeGore = 911;
			TreeTop = "TreeOceanTop";
			TreeBottom = "TreeOceanBottom";
			break;
		case Tree.Snow:
			treeGore = 913;
			TreeTop = "TreeSnowTop";
			TreeBottom = "TreeSnowBottom";
			break;
		case Tree.SulphurousSea:
			treeGore = ModContent.GoreType<SulphurLeaf>();
			TreeTop = "TreeSulphurousSeaTop";
			TreeBottom = "TreeSulphurousSeaBottom";
			break;
		}
		if (!Main.dedServ)
		{
			for (int i = 0; i < 20; i++)
			{
				Vector2 velocity2 = CalamityUtils.RandomVelocity(100f, 70f, 100f);
				Vector2 spawnSource = base.Projectile.Center;
				spawnSource.X += Main.rand.Next(-base.Projectile.width / 2, base.Projectile.width / 2 + 1);
				spawnSource.Y += Main.rand.Next(-base.Projectile.height / 2, base.Projectile.height / 2 + 1);
				int idx = Gore.NewGore(base.Projectile.GetSource_FromThis(), spawnSource, velocity2, treeGore);
				Main.gore[idx].velocity.X *= Main.rand.NextFloat(0.5f, 5f);
				Main.gore[idx].velocity.Y *= Main.rand.NextFloat(0.5f, 5f);
				Main.gore[idx].velocity.X += Main.rand.NextFloat(-5f, 5f);
				Main.gore[idx].velocity.Y += Main.rand.NextFloat(-5f, 5f);
				Main.gore[idx].scale *= Main.rand.NextFloat(0.8f, 1.2f);
			}
			Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.Center, CalamityUtils.RandomVelocity(100f, 70f, 100f) * Main.rand.NextFloat(), base.Mod.Find<ModGore>(TreeTop).Type, base.Projectile.scale);
			Gore.NewGore(base.Projectile.GetSource_FromThis(), base.Projectile.Center, CalamityUtils.RandomVelocity(100f, 70f, 100f) * Main.rand.NextFloat(), base.Mod.Find<ModGore>(TreeBottom).Type, base.Projectile.scale);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_012b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0130: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_013c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0141: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0152: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0171: Unknown result type (might be due to invalid IL or missing references)
		//IL_0184: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_018d: Unknown result type (might be due to invalid IL or missing references)
		//IL_019a: Unknown result type (might be due to invalid IL or missing references)
		//IL_019f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01af: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.ai[1] == 0f)
		{
			return false;
		}
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		switch (TreeType)
		{
		case Tree.Astral:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/TreeAstral", (AssetRequestMode)2).Value;
			break;
		case Tree.Corruption:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/TreeCorruption", (AssetRequestMode)2).Value;
			break;
		case Tree.Crimson:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/TreeCrimson", (AssetRequestMode)2).Value;
			break;
		case Tree.Forest:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/TreeForest", (AssetRequestMode)2).Value;
			break;
		case Tree.Hallow:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/TreeHallow", (AssetRequestMode)2).Value;
			break;
		case Tree.Jungle:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/TreeJungle", (AssetRequestMode)2).Value;
			break;
		case Tree.Ocean:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/TreeOcean", (AssetRequestMode)2).Value;
			break;
		case Tree.Snow:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/TreeSnow", (AssetRequestMode)2).Value;
			break;
		case Tree.SulphurousSea:
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/TreeSulphurousSea", (AssetRequestMode)2).Value;
			break;
		}
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == 1);
		Main.spriteBatch.Draw(texture, drawPosition, (Rectangle?)frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction, 0f);
		return false;
	}

	public override bool? CanDamage()
	{
		if (base.Projectile.ai[1] != 0f)
		{
			return null;
		}
		return false;
	}
}
