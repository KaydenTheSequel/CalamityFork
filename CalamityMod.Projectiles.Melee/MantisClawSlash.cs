using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using CalamityMod.Buffs.DamageOverTime;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Melee;

public class MantisClawSlash : ModProjectile, ILocalizedModType, IModType
{
	private const int TimerCap = 20;

	private Color startColor;

	private Color endColor;

	private int dir = 1;

	public new string LocalizationCategory => "Projectiles.Melee";

	public override string Texture => "CalamityMod/Particles/SlashSmear";

	public override void SetDefaults()
	{
		base.Projectile.usesLocalNPCImmunity = true;
		base.Projectile.localNPCHitCooldown = 60;
		base.Projectile.timeLeft = 20;
		base.Projectile.tileCollide = false;
		base.Projectile.width = 256;
		base.Projectile.height = 256;
		base.Projectile.friendly = true;
		base.Projectile.penetrate = -1;
		base.Projectile.DamageType = TrueMeleeDamageClass.Instance;
	}

	public override void OnSpawn(IEntitySource source)
	{
		//IL_0071: Unknown result type (might be due to invalid IL or missing references)
		//IL_0076: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0098: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00d7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fe: Unknown result type (might be due to invalid IL or missing references)
		//IL_0116: Unknown result type (might be due to invalid IL or missing references)
		//IL_011b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		base.Projectile.scale = 0f;
		base.Projectile.ai[2] = Main.rand.NextFloat(0.5f, 1.25f);
		if (Main.rand.NextBool(2))
		{
			dir = -1;
		}
		int num = 5;
		List<Color> list = new List<Color>(num);
		CollectionsMarshal.SetCount(list, num);
		Span<Color> span = CollectionsMarshal.AsSpan(list);
		int num2 = 0;
		span[num2] = new Color(248, 197, 58);
		num2++;
		span[num2] = new Color(143, 208, 50);
		num2++;
		span[num2] = new Color(69, 114, 227);
		num2++;
		span[num2] = new Color(212, 128, 187);
		num2++;
		span[num2] = new Color(255, 140, 82);
		List<Color> ColorList = list;
		startColor = ColorList[Main.rand.Next(ColorList.Count)];
		endColor = ColorList[Main.rand.Next(ColorList.Count)];
	}

	public override void ModifyDamageHitbox(ref Rectangle hitbox)
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_001b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0033: Unknown result type (might be due to invalid IL or missing references)
		//IL_0038: Unknown result type (might be due to invalid IL or missing references)
		hitbox = new Rectangle((int)base.Projectile.Center.X - 65, (int)base.Projectile.Center.Y - 65, 130, 130);
	}

	public override void AI()
	{
		//IL_0007: Unknown result type (might be due to invalid IL or missing references)
		//IL_0011: Unknown result type (might be due to invalid IL or missing references)
		//IL_0016: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b2: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b3: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b8: Unknown result type (might be due to invalid IL or missing references)
		Projectile projectile = base.Projectile;
		projectile.velocity *= 0.9f;
		if (base.Projectile.timeLeft > 10)
		{
			base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, base.Projectile.ai[2], 0.1f);
		}
		else
		{
			base.Projectile.scale = MathHelper.Lerp(base.Projectile.scale, base.Projectile.ai[2], -0.1f);
		}
		base.Projectile.velocity = base.Projectile.velocity.RotatedBy(MathHelper.ToRadians(3f) * (float)dir);
		base.Projectile.ai[1]++;
		base.Projectile.ai[0] = MathHelper.Lerp(base.Projectile.ai[0], (float)Math.PI * 2f * (float)dir, 0.15f);
	}

	public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
	}

	public override void OnHitPlayer(Player target, Player.HurtInfo info)
	{
		target.AddBuff(ModContent.BuffType<HeavyBleeding>(), 180);
	}

	public override bool PreDraw(ref Color lightColor)
	{
		//IL_0024: Unknown result type (might be due to invalid IL or missing references)
		//IL_0029: Unknown result type (might be due to invalid IL or missing references)
		//IL_002e: Unknown result type (might be due to invalid IL or missing references)
		//IL_003a: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_007c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0081: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c0: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ca: Unknown result type (might be due to invalid IL or missing references)
		Asset<Texture2D> tex = ModContent.Request<Texture2D>(Texture, (AssetRequestMode)2);
		for (float i = 0f; i < 1f; i += 0.33f)
		{
			Main.EntitySpriteDraw(tex.Value, base.Projectile.Center - Main.screenPosition, tex.Frame(), Color.Lerp(startColor, endColor, base.Projectile.ai[1] / 20f).MultiplyRGBA(new Color(255f, 255f, 255f, 0f)), base.Projectile.rotation - ((dir == -1) ? MathHelper.ToRadians(-135f) : MathHelper.ToRadians(180f)) + base.Projectile.ai[0], tex.Size() / 2f, MathHelper.Lerp(0.6f, 1f, i) * base.Projectile.scale, (SpriteEffects)((dir != 1) ? 2 : 0));
		}
		return false;
	}
}
