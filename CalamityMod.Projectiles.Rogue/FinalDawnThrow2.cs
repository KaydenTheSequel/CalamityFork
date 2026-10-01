using CalamityMod.Dusts;
using CalamityMod.Items.Weapons.Rogue;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class FinalDawnThrow2 : ModProjectile, ILocalizedModType, IModType
{
	private bool HasHitEnemy;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.TrailCacheLength[base.Type] = 8;
		ProjectileID.Sets.TrailingMode[base.Type] = 0;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = 200;
		base.Projectile.height = 200;
		base.Projectile.friendly = true;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.ignoreWater = true;
		base.Projectile.penetrate = -1;
		base.Projectile.light = 0f;
		base.Projectile.extraUpdates = 1;
		base.Projectile.tileCollide = true;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = base.Projectile.MaxUpdates * 15;
	}

	public override bool TileCollideStyle(ref int width, ref int height, ref bool fallThrough, ref Vector2 hitboxCenterFrac)
	{
		width = 32;
		height = 32;
		return true;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_007b: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player.immuneTime <= 30)
		{
			player.immuneNoBlink = true;
			player.immuneTime = 30;
		}
		if (Main.myPlayer == base.Projectile.owner && !HasHitEnemy)
		{
			for (int i = 0; i < 6; i++)
			{
				Vector2 velocity = Main.rand.NextVector2Circular(7.2f, 7.2f);
				Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), base.Projectile.Center, velocity, ModContent.ProjectileType<FinalDawnFireball>(), (int)((double)base.Projectile.damage * 0.2), base.Projectile.knockBack, base.Projectile.owner, 0f, target.whoAmI);
			}
			HasHitEnemy = true;
		}
	}

	public override void AI()
	{
		//IL_0047: Unknown result type (might be due to invalid IL or missing references)
		//IL_0052: Unknown result type (might be due to invalid IL or missing references)
		//IL_0108: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_011a: Unknown result type (might be due to invalid IL or missing references)
		//IL_011f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0124: Unknown result type (might be due to invalid IL or missing references)
		//IL_0178: Unknown result type (might be due to invalid IL or missing references)
		//IL_018f: Unknown result type (might be due to invalid IL or missing references)
		//IL_01a6: Unknown result type (might be due to invalid IL or missing references)
		//IL_022c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0259: Unknown result type (might be due to invalid IL or missing references)
		//IL_025f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0279: Unknown result type (might be due to invalid IL or missing references)
		//IL_0283: Unknown result type (might be due to invalid IL or missing references)
		//IL_0288: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c7: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		if (player == null || player.dead)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.localAI[0] == 0f)
		{
			SoundEngine.PlaySound(in TheFinalDawn.UseSound, base.Projectile.position);
			base.Projectile.localAI[0] = 1f;
		}
		ActiveEntityIterator<Projectile>.Enumerator enumerator = Main.ActiveProjectiles.GetEnumerator();
		while (enumerator.MoveNext())
		{
			Projectile proj = enumerator.Current;
			if (proj.owner == player.whoAmI && proj.aiStyle == 7 && proj.aiStyle == 7)
			{
				proj.Kill();
			}
		}
		base.Projectile.spriteDirection = ((base.Projectile.velocity.X > 0f) ? 1 : (-1));
		base.Projectile.rotation += 0.25f * (float)base.Projectile.direction;
		player.Center = base.Projectile.Center;
		player.fullRotationOrigin = player.Center - player.position;
		player.fullRotation = base.Projectile.rotation;
		player.ChangeDir(base.Projectile.direction);
		player.heldProj = base.Projectile.whoAmI;
		player.bodyFrame.Y = player.bodyFrame.Height;
		bool worldEdge = base.Projectile.Center.X < 1000f || base.Projectile.Center.Y < 1000f || base.Projectile.Center.X > (float)(Main.maxTilesX * 16 - 1000) || base.Projectile.Center.Y > (float)(Main.maxTilesY * 16 - 1000);
		base.Projectile.ai[0]++;
		if ((base.Projectile.ai[0] >= 60f) | worldEdge)
		{
			base.Projectile.Kill();
		}
		int idx = Dust.NewDust(base.Projectile.position, base.Projectile.width, base.Projectile.height, ModContent.DustType<FinalFlame>(), 0f, 0f, 0, default(Color), 2.5f);
		Main.dust[idx].velocity = base.Projectile.velocity * -0.5f;
		Main.dust[idx].noGravity = true;
		Main.dust[idx].noLight = false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00a3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00a8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ed: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f3: Unknown result type (might be due to invalid IL or missing references)
		//IL_0123: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0147: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0172: Unknown result type (might be due to invalid IL or missing references)
		Player player = Main.player[base.Projectile.owner];
		float scytheRotation = player.fullRotation;
		Texture2D scytheTexture = TextureAssets.Projectile[base.Type].Value;
		Texture2D glowScytheTexture = ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/FinalDawnThrow2_Glow", (AssetRequestMode)2).Value;
		int num214 = TextureAssets.Projectile[base.Type].Value.Height / Main.projFrames[base.Type];
		int y6 = num214 * base.Projectile.frame;
		Vector2 origin = default(Vector2);
		((Vector2)(ref origin))._002Ector((float)scytheTexture.Width / 2f + 40f * (float)player.direction, (float)num214 * 1.1f);
		Main.spriteBatch.Draw(scytheTexture, player.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, (Rectangle?)new Rectangle(0, y6, scytheTexture.Width, num214), base.Projectile.GetAlpha(lightColor), scytheRotation, origin, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		Main.spriteBatch.Draw(glowScytheTexture, player.Center - Main.screenPosition + Vector2.UnitY * base.Projectile.gfxOffY, (Rectangle?)new Rectangle(0, y6, scytheTexture.Width, num214), base.Projectile.GetAlpha(Color.White), scytheRotation, origin, base.Projectile.scale, (SpriteEffects)(base.Projectile.spriteDirection != 1), 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		Main.player[base.Projectile.owner].fullRotation = 0f;
	}
}
