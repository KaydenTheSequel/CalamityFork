using CalamityMod.NPCs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

[PierceResistException(false)]
public class MetalShard : ModProjectile, ILocalizedModType, IModType
{
	public bool Stuck;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override void SetDefaults()
	{
		base.Projectile.friendly = true;
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.penetrate = 2;
		base.Projectile.extraUpdates = 1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
	}

	public override void AI()
	{
		if (!Stuck)
		{
			if (base.Projectile.ai[0] == 0f)
			{
				base.Projectile.rotation += 0.1f;
			}
			base.Projectile.velocity.Y += 0.1f;
			if (base.Projectile.velocity.Y > 16f)
			{
				base.Projectile.velocity.Y = 16f;
			}
		}
		base.Projectile.StickyProjAI(15);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		Stuck = true;
	}

	public override void ModifyHitNPC(NPC target, ref NPC.HitModifiers modifiers)
	{
		base.Projectile.ModifyHitNPCSticky(8);
	}

	public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
	{
		//IL_0000: Unknown result type (might be due to invalid IL or missing references)
		//IL_0009: Unknown result type (might be due to invalid IL or missing references)
		//IL_0014: Unknown result type (might be due to invalid IL or missing references)
		//IL_001d: Unknown result type (might be due to invalid IL or missing references)
		if (targetHitbox.Width > 8 && targetHitbox.Height > 8)
		{
			((Rectangle)(ref targetHitbox)).Inflate(-targetHitbox.Width / 8, -targetHitbox.Height / 8);
		}
		return null;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0091: Unknown result type (might be due to invalid IL or missing references)
		//IL_0096: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.localAI[1] == 0f)
		{
			base.Projectile.localAI[1] = Main.rand.Next(1, 4);
		}
		float num = base.Projectile.localAI[1];
		Texture2D texture = ((num == 2f) ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/MetalShard2", (AssetRequestMode)2).Value : ((num != 3f) ? ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/MetalShard", (AssetRequestMode)2).Value : ModContent.Request<Texture2D>("CalamityMod/Projectiles/Rogue/MetalShard3", (AssetRequestMode)2).Value));
		Main.spriteBatch.Draw(texture, base.Projectile.Center - Main.screenPosition, (Rectangle?)new Rectangle(0, 0, texture.Width, texture.Height), base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, new Vector2((float)texture.Width / 2f, (float)texture.Height / 2f), base.Projectile.scale, (SpriteEffects)0, 0f);
		return false;
	}

	public override void OnKill(int timeLeft)
	{
		//IL_000a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0034: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		for (int i = 0; i < 6; i++)
		{
			Dust.NewDust(base.Projectile.Center, base.Projectile.width, base.Projectile.height, 82);
		}
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.Center);
		return true;
	}
}
