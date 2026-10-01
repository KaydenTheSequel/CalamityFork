using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class OrichalcumSpikedGemstoneProjectile : ModProjectile, ILocalizedModType, IModType
{
	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Items/Weapons/Rogue/OrichalcumSpikedGemstone";

	public override void SetDefaults()
	{
		base.Projectile.width = 12;
		base.Projectile.height = 12;
		base.Projectile.friendly = true;
		base.Projectile.aiStyle = 2;
		base.Projectile.penetrate = 4;
		base.Projectile.timeLeft = 360;
		base.AIType = 48;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 10;
	}

	public override bool OnTileCollide(Vector2 oldVelocity)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_001c: Unknown result type (might be due to invalid IL or missing references)
		//IL_009b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cf: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ae: Unknown result type (might be due to invalid IL or missing references)
		//IL_0044: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_007a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ef: Unknown result type (might be due to invalid IL or missing references)
		Vector2 velocity = base.Projectile.velocity;
		if (base.Projectile.velocity.Y != velocity.Y && (velocity.Y < -3f || velocity.Y > 3f))
		{
			Collision.HitTiles(base.Projectile.position, base.Projectile.velocity, base.Projectile.width, base.Projectile.height);
			SoundEngine.PlaySound(in SoundID.Dig, base.Projectile.Center);
		}
		if (base.Projectile.velocity.X != velocity.X)
		{
			base.Projectile.velocity.X = velocity.X * -0.5f;
		}
		if (base.Projectile.velocity.Y != velocity.Y && velocity.Y > 1f)
		{
			base.Projectile.velocity.Y = velocity.Y * -0.5f;
		}
		return false;
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0019: Unknown result type (might be due to invalid IL or missing references)
		//IL_001e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0023: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		Main.EntitySpriteDraw(tex, base.Projectile.Center - Main.screenPosition, null, base.Projectile.GetAlpha(lightColor), base.Projectile.rotation, tex.Size() / 2f, base.Projectile.scale, (SpriteEffects)0);
		return false;
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffect(target.Center);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		//IL_0002: Unknown result type (might be due to invalid IL or missing references)
		OnHitEffect(target.Center);
	}

	private void OnHitEffect(Vector2 targetPos)
	{
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_007f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0080: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_0106: Unknown result type (might be due to invalid IL or missing references)
		//IL_0107: Unknown result type (might be due to invalid IL or missing references)
		if (Main.myPlayer != base.Projectile.owner || !base.Projectile.Calamity().stealthStrike)
		{
			return;
		}
		Vector2 startPos = default(Vector2);
		for (int i = 0; i < 2; i++)
		{
			int direction = Main.player[base.Projectile.owner].direction;
			float xStart = Main.screenPosition.X;
			if (direction < 0)
			{
				xStart += (float)Main.screenWidth;
			}
			float yStart = Main.screenPosition.Y + (float)Main.rand.Next(Main.screenHeight);
			((Vector2)(ref startPos))._002Ector(xStart, yStart);
			Vector2 pathToTravel = targetPos - startPos;
			pathToTravel.X += Main.rand.NextFloat(-50f, 50f) * 0.1f;
			pathToTravel.Y += Main.rand.NextFloat(-50f, 50f) * 0.1f;
			float speedMult = 24f / ((Vector2)(ref pathToTravel)).Length();
			pathToTravel.X *= speedMult;
			pathToTravel.Y *= speedMult;
			int petal = Projectile.NewProjectile(base.Projectile.GetSource_FromThis(), startPos, pathToTravel, 221, base.Projectile.damage, 0f, base.Projectile.owner);
			if (petal.WithinBounds(Main.maxProjectiles))
			{
				Main.projectile[petal].DamageType = RogueDamageClass.Instance;
			}
		}
	}
}
