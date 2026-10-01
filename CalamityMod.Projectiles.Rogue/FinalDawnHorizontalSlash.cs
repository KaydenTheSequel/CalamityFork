using CalamityMod.Buffs.DamageOverTime;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FinalDawnHorizontalSlash : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		Main.projFrames[base.Type] = 9;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 600;
		base.Projectile.height = 156;
		base.Projectile.friendly = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ownerHitCheck = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override void AI()
	{
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fc: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		//IL_0131: Unknown result type (might be due to invalid IL or missing references)
		//IL_0136: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player == null || player.dead)
		{
			base.Projectile.Kill();
		}
		base.Projectile.Center = player.Center;
		player.heldProj = base.Projectile.whoAmI;
		base.Projectile.spriteDirection = (base.Projectile.velocity.X > 0f).ToDirectionInt();
		base.Projectile.ai[0]++;
		if (base.Projectile.ai[0] >= 4f)
		{
			base.Projectile.ai[1]++;
			base.Projectile.ai[0] = 0f;
			if (base.Projectile.ai[1] == 3f)
			{
				base.Projectile.friendly = true;
				SoundEngine.PlaySound(in TheFinalDawn.UseSound, base.Projectile.position);
				if (base.Projectile.owner == Main.myPlayer)
				{
					Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, Vector2.Zero, ModContent.ProjectileType<FinalDawnFlame>(), base.Projectile.damage / 2, 0f, base.Projectile.owner);
				}
			}
		}
		if (base.Projectile.ai[1] >= 9f)
		{
			base.Projectile.Kill();
			return;
		}
		base.Projectile.frame = (int)base.Projectile.ai[1];
		if (base.Projectile.ai[1] < 2f)
		{
			player.bodyFrame.Y = player.bodyFrame.Height;
		}
		else
		{
			player.bodyFrame.Y = 3 * player.bodyFrame.Height;
		}
		if (base.Projectile.ai[1] == 4f || base.Projectile.ai[1] == 5f)
		{
			player.ChangeDir(-1 * base.Projectile.spriteDirection);
		}
		else
		{
			player.ChangeDir(base.Projectile.spriteDirection);
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0061: Unknown result type (might be due to invalid IL or missing references)
		//IL_0066: Unknown result type (might be due to invalid IL or missing references)
		//IL_006b: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ce: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_010b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0110: Unknown result type (might be due to invalid IL or missing references)
		//IL_0120: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0138: Unknown result type (might be due to invalid IL or missing references)
		//IL_0148: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		Texture2D scytheTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D scytheGlowTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/FinalDawnHorizontalSlash_Glow", (AssetRequestMode)2).Value;
		int height = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int yStart = height * base.Projectile.frame;
		Main.spriteBatch.Draw(scytheTexture, base.Projectile.Center - Main.screenPosition + base.Projectile.gfxOffY * Vector2.UnitY, (Rectangle?)new Rectangle(0, yStart, scytheTexture.Width, height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)scytheTexture.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		Main.spriteBatch.Draw(scytheGlowTexture, base.Projectile.Center - Main.screenPosition + base.Projectile.gfxOffY * Vector2.UnitY, (Rectangle?)new Rectangle(0, yStart, scytheTexture.Width, height), base.Projectile.GetAlpha(Color.White), base.Projectile.rotation, new Vector2((float)scytheTexture.Width / 2f, (float)height / 2f), base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<Dragonfire>(), 300);
	}
}
