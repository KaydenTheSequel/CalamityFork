using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Rogue;

public class CircletTornado : ModProjectile, ILocalizedModType, IModType
{
	public static float Lifetime = 900f;

	public static float Fadetime = 120f;

	public new string LocalizationCategory => "Projectiles.Rogue";

	public override string Texture => "CalamityMod/Projectiles/TornadoProj";

	public ref float Time => ref base.Projectile.ai[0];

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 10);
		base.Projectile.friendly = true;
		base.Projectile.tileCollide = false;
		base.Projectile.DamageType = RogueDamageClass.Instance;
		base.Projectile.minion = true;
		base.Projectile.penetrate = -1;
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 30;
	}

	public override void AI()
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_002f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00cc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f6: Unknown result type (might be due to invalid IL or missing references)
		//IL_0125: Unknown result type (might be due to invalid IL or missing references)
		//IL_012a: Unknown result type (might be due to invalid IL or missing references)
		//IL_013d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0142: Unknown result type (might be due to invalid IL or missing references)
		//IL_0249: Unknown result type (might be due to invalid IL or missing references)
		//IL_024a: Unknown result type (might be due to invalid IL or missing references)
		//IL_024b: Unknown result type (might be due to invalid IL or missing references)
		//IL_024d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0257: Unknown result type (might be due to invalid IL or missing references)
		//IL_025c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0261: Unknown result type (might be due to invalid IL or missing references)
		//IL_0263: Unknown result type (might be due to invalid IL or missing references)
		//IL_0268: Unknown result type (might be due to invalid IL or missing references)
		//IL_026a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0280: Unknown result type (might be due to invalid IL or missing references)
		//IL_0286: Unknown result type (might be due to invalid IL or missing references)
		//IL_0296: Unknown result type (might be due to invalid IL or missing references)
		//IL_0298: Unknown result type (might be due to invalid IL or missing references)
		//IL_029f: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a0: Unknown result type (might be due to invalid IL or missing references)
		//IL_02a2: Unknown result type (might be due to invalid IL or missing references)
		//IL_02c9: Unknown result type (might be due to invalid IL or missing references)
		//IL_014d: Unknown result type (might be due to invalid IL or missing references)
		//IL_014e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0151: Unknown result type (might be due to invalid IL or missing references)
		//IL_0156: Unknown result type (might be due to invalid IL or missing references)
		//IL_0158: Unknown result type (might be due to invalid IL or missing references)
		//IL_015c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0167: Unknown result type (might be due to invalid IL or missing references)
		//IL_016b: Unknown result type (might be due to invalid IL or missing references)
		if (base.Projectile.soundDelay == 0)
		{
			base.Projectile.soundDelay = -1;
			SoundEngine.PlaySound(in SoundID.Item82, base.Projectile.Center);
		}
		Time++;
		if (Time >= Lifetime)
		{
			base.Projectile.Kill();
		}
		if (base.Projectile.numHits >= 30)
		{
			base.Projectile.damage = 0;
			if (Time < Lifetime - Fadetime)
			{
				Time = Lifetime - Fadetime + Time % 60f;
				base.Projectile.netUpdate = true;
			}
		}
		GetVerticallyExpandedPos(out var newTop, out var newBottom, out var newCenter, out var newSize);
		base.Projectile.width = (int)(newSize.X * 0.65f);
		base.Projectile.height = (int)newSize.Y;
		base.Projectile.Center = newCenter;
		if (base.Projectile.owner == Main.myPlayer)
		{
			bool breakFlag = false;
			Vector2 playerCenter = Main.player[base.Projectile.owner].Center;
			Vector2 playerTop = Main.player[base.Projectile.owner].Top;
			for (float i = 0f; i < 1f; i += 0.05f)
			{
				Vector2 collisionPos = Vector2.Lerp(newTop, newBottom, i);
				if (Collision.CanHitLine(collisionPos, 0, 0, playerCenter, 0, 0) || Collision.CanHitLine(collisionPos, 0, 0, playerTop, 0, 0))
				{
					breakFlag = true;
					break;
				}
			}
			if (!breakFlag && Time < Lifetime - Fadetime)
			{
				Time = Lifetime - Fadetime + Time % 60f;
				base.Projectile.netUpdate = true;
			}
		}
		if (Time < Lifetime - Fadetime)
		{
			float randFactor = Main.rand.NextFloat();
			Vector2 randomOffset = default(Vector2);
			((Vector2)(ref randomOffset))._002Ector(MathHelper.Lerp(0.1f, 1f, Main.rand.NextFloat()) * MathHelper.Lerp(-2.2f, -0.6f, randFactor), MathHelper.Lerp(-0.5f, 0.9f, randFactor));
			Vector2 fixedOffset = default(Vector2);
			((Vector2)(ref fixedOffset))._002Ector(6f, 10f);
			Vector2 dustPos = newCenter + newSize * randomOffset * 0.5f + fixedOffset;
			Dust sand = Dust.NewDustDirect(dustPos, 0, 0, 269);
			sand.position = dustPos;
			sand.customData = newCenter + fixedOffset;
			sand.fadeIn = 1f;
			sand.scale = 0.3f;
			if (randomOffset.X > -1.2f)
			{
				sand.velocity.X = 1f + Main.rand.NextFloat();
			}
			sand.velocity.Y = Main.rand.NextFloat() * -0.5f - 1f;
		}
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_00ad: Unknown result type (might be due to invalid IL or missing references)
		//IL_0191: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ec: Unknown result type (might be due to invalid IL or missing references)
		//IL_010f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0114: Unknown result type (might be due to invalid IL or missing references)
		//IL_012c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0134: Unknown result type (might be due to invalid IL or missing references)
		//IL_0139: Unknown result type (might be due to invalid IL or missing references)
		//IL_013e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0143: Unknown result type (might be due to invalid IL or missing references)
		//IL_014b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0157: Unknown result type (might be due to invalid IL or missing references)
		//IL_015b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0166: Unknown result type (might be due to invalid IL or missing references)
		//IL_0170: Unknown result type (might be due to invalid IL or missing references)
		Texture2D tex = TextureAssets.Projectile[base.Type].Value;
		GetVerticallyExpandedPos(out var newTop, out var newBottom, out var _, out var newSize);
		float TextureFadetime = Lifetime - 60f;
		Color fullColor = default(Color);
		((Color)(ref fullColor))._002Ector(212, 192, 100);
		float colorMult = ((Time > TextureFadetime) ? MathHelper.Lerp(1f, 0f, (Time - TextureFadetime) / 60f) : MathHelper.Clamp(Time / 30f, 0f, 1f));
		float timedRotation = Time * (float)Math.PI * -0.02f;
		float incrementStorage = 0f;
		float increment = 5.1f;
		for (float k = newBottom.Y; k > newTop.Y; k -= increment)
		{
			incrementStorage += increment;
			float segmentHeight = incrementStorage / newSize.Y;
			float addedRotation = incrementStorage * (float)Math.PI * -0.1f;
			float addedScale = segmentHeight - 0.15f;
			Color drawColor = Color.Lerp(Color.Transparent, fullColor, (segmentHeight > 0.5f) ? (2f - segmentHeight * 2f) : (segmentHeight * 2f));
			((Color)(ref drawColor)).A = (byte)((float)(int)((Color)(ref drawColor)).A * 0.5f);
			Vector2 drawPos = new Vector2(newBottom.X, k) - Main.screenPosition;
			Main.spriteBatch.Draw(tex, drawPos, (Rectangle?)null, drawColor * colorMult, timedRotation + addedRotation, tex.Size() * 0.5f, 1f + addedScale, (SpriteEffects)0, 0f);
		}
		return false;
	}

	public void GetVerticallyExpandedPos(out Vector2 newTop, out Vector2 newBottom, out Vector2 newCenter, out Vector2 newSize)
	{
		//IL_0006: Unknown result type (might be due to invalid IL or missing references)
		//IL_000b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0010: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0017: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0036: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_004a: Unknown result type (might be due to invalid IL or missing references)
		//IL_004f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_005a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0090: Unknown result type (might be due to invalid IL or missing references)
		//IL_009a: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00bd: Unknown result type (might be due to invalid IL or missing references)
		Point center = base.Projectile.Center.ToTileCoordinates();
		Collision.ExpandVertically(center.X, center.Y, out var topY, out var bottomY, 15, 15);
		newTop = new Vector2((float)center.X, (float)(topY + 1)) * 16f + new Vector2(8f);
		newBottom = new Vector2((float)center.X, (float)(bottomY - 1)) * 16f + new Vector2(8f);
		newCenter = Vector2.Lerp(newTop, newBottom, 0.5f);
		newSize = new Vector2(0f, newBottom.Y - newTop.Y);
		newSize.X = newSize.Y * 0.2f;
	}
}
