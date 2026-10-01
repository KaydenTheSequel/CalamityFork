using System;
using System.Collections.Generic;
using CalamityMod.Buffs.Summon;
using CalamityMod.CalPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.GameContent;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Summon.Umbrella;

public class MagicHat : ModProjectile, ILocalizedModType, IModType
{
	public const float Range = 1500.0001f;

	public new string LocalizationCategory => "Projectiles.Summon";

	public override void SetStaticDefaults()
	{
		Main.projPet[base.Type] = true;
		ProjectileID.Sets.MinionSacrificable[base.Type] = true;
		ProjectileID.Sets.MinionTargettingFeature[base.Type] = true;
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 1;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 30);
		base.Projectile.netImportant = true;
		base.Projectile.friendly = true;
		base.Projectile.ignoreWater = true;
		base.Projectile.minionSlots = 5f;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.minion = true;
		base.Projectile.DamageType = DamageClass.Summon;
	}

	public override void AI()
	{
		//IL_01eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01f0: Unknown result type (might be due to invalid IL or missing references)
		//IL_0201: Unknown result type (might be due to invalid IL or missing references)
		//IL_0206: Unknown result type (might be due to invalid IL or missing references)
		//IL_0224: Unknown result type (might be due to invalid IL or missing references)
		//IL_0229: Unknown result type (might be due to invalid IL or missing references)
		//IL_023a: Unknown result type (might be due to invalid IL or missing references)
		//IL_023f: Unknown result type (might be due to invalid IL or missing references)
		//IL_010e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0113: Unknown result type (might be due to invalid IL or missing references)
		//IL_0305: Unknown result type (might be due to invalid IL or missing references)
		//IL_0335: Unknown result type (might be due to invalid IL or missing references)
		//IL_033b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0352: Unknown result type (might be due to invalid IL or missing references)
		//IL_035c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0361: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		CalamityPlayer modPlayer = player.Calamity();
		base.Projectile.Calamity();
		bool num = base.Projectile.type == ModContent.ProjectileType<MagicHat>();
		player.AddBuff(ModContent.BuffType<MagicHatBuff>(), 3600);
		if (num)
		{
			if (player.dead)
			{
				modPlayer.magicHat = false;
			}
			if (modPlayer.magicHat)
			{
				base.Projectile.timeLeft = 2;
			}
		}
		if (base.Projectile.ai[0] == 1f)
		{
			List<Tuple<int, float>> Projectiles = new List<Tuple<int, float>>
			{
				new Tuple<int, float>(ModContent.ProjectileType<MagicArrow>(), 2f),
				new Tuple<int, float>(ModContent.ProjectileType<MagicHammer>(), 3f),
				new Tuple<int, float>(ModContent.ProjectileType<MagicAxe>(), 1f),
				new Tuple<int, float>(ModContent.ProjectileType<MagicUmbrella>(), 1f),
				new Tuple<int, float>(ModContent.ProjectileType<MagicRifle>(), 1f)
			};
			for (int i = 0; i < Projectiles.Count; i++)
			{
				int p = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, Projectiles[i].Item1, (int)((float)base.Projectile.damage * Projectiles[i].Item2), base.Projectile.knockBack * Projectiles[i].Item2, base.Projectile.owner, base.Projectile.whoAmI);
				if (Main.projectile.IndexInRange(p))
				{
					Main.projectile[p].originalDamage = (int)((float)base.Projectile.originalDamage * Projectiles[i].Item2);
				}
			}
		}
		base.Projectile.ai[0]++;
		base.Projectile.Center = player.MountedCenter + Vector2.UnitY * (player.gfxOffY - 30f);
		if (player.gravDir == -1f)
		{
			base.Projectile.Center = player.MountedCenter - Vector2.UnitY * (player.gfxOffY - 30f);
			base.Projectile.rotation = (float)Math.PI;
		}
		else
		{
			base.Projectile.rotation = 0f;
		}
		base.Projectile.position.X = (int)base.Projectile.position.X;
		base.Projectile.position.Y = (int)base.Projectile.position.Y;
		float scalar = (float)(int)Main.mouseTextColor / 200f - 0.35f;
		scalar *= 0.2f;
		base.Projectile.scale = scalar + 0.95f;
		if (base.Projectile.localAI[0] == 0f)
		{
			int dustAmt = 50;
			for (int dustIndex = 0; dustIndex < dustAmt; dustIndex++)
			{
				int dustEffects = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height - 16, 234);
				Dust obj = Main.dust[dustEffects];
				obj.velocity *= 2f;
				Main.dust[dustEffects].scale *= 1.15f;
			}
			base.Projectile.localAI[0]++;
		}
	}

	public override Color? GetAlpha(Color lightColor)
	{
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		return new Color(200, 200, 200, 200);
	}

	public override void DrawBehind(int index, List<int> behindNPCsAndTiles, List<int> behindNPCs, List<int> behindProjectiles, List<int> overPlayers, List<int> overWiresUI)
	{
		overPlayers.Add(index);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00c1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e1: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00eb: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0105: Unknown result type (might be due to invalid IL or missing references)
		//IL_010a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0200: Unknown result type (might be due to invalid IL or missing references)
		//IL_020e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0213: Unknown result type (might be due to invalid IL or missing references)
		//IL_0223: Unknown result type (might be due to invalid IL or missing references)
		//IL_0230: Unknown result type (might be due to invalid IL or missing references)
		//IL_015d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0189: Unknown result type (might be due to invalid IL or missing references)
		//IL_018b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0199: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_01bb: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ec: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		Texture2D texture = TextureAssets.Projectile[base.Type].Value;
		float stealthPercent = ((player.Calamity().rogueStealthMax != 0f) ? (player.Calamity().rogueStealth / player.Calamity().rogueStealthMax) : 0f);
		bool hasStealth = player.Calamity().rogueStealth > 0f && stealthPercent > 0.5f && player.townNPCs < 3f && CalamityClientConfig.Instance.StealthInvisibility;
		if (player.ShouldNotDraw | hasStealth)
		{
			texture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Summon/Umbrella/MagicHatInvis", (AssetRequestMode)2).Value;
		}
		Rectangle frame = texture.Frame(1, Main.projFrames[base.Type], 0, base.Projectile.frame);
		Vector2 origin = frame.Size() * 0.5f;
		Vector2 drawPosition = base.Projectile.Center - Main.screenPosition + new Vector2(0f, base.Projectile.gfxOffY);
		SpriteEffects direction = (SpriteEffects)(base.Projectile.spriteDirection == 1);
		if (player.dye[0].dye > 0)
		{
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)1, BlendState.AlphaBlend, SamplerState.LinearClamp, DepthStencilState.Default, RasterizerState.CullNone, (Effect)null, Main.GameViewMatrix.ZoomMatrix);
			GameShaders.Armor.GetShaderFromItemId(player.dye[0].type).Apply();
			Main.spriteBatch.Draw(texture, drawPosition, (Rectangle?)frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction, 0f);
			Main.spriteBatch.End();
			Main.spriteBatch.Begin((SpriteSortMode)0, BlendState.AlphaBlend, Main.DefaultSamplerState, DepthStencilState.None, Main.Rasterizer, (Effect)null, Main.Transform);
		}
		else
		{
			Main.spriteBatch.Draw(texture, drawPosition, (Rectangle?)frame, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, origin, base.Projectile.scale, direction, 0f);
		}
		return false;
	}
}
