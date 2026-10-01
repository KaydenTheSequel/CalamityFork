using CalamityMod.NPCs.SupremeCalamitas;
using CalamityMod.Particles;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.Projectiles.Typeless;

public class SCalAltarArenaVisual : ModProjectile, ILocalizedModType, IModType
{
	private const int VisualDuration = 900;

	private Particle topLine;

	private Particle bottomLine;

	private Particle leftLine;

	private Particle rightLine;

	public new string LocalizationCategory => "Projectiles.Typeless";

	public override string Texture => "CalamityMod/Projectiles/InvisibleProj";

	public override void SetStaticDefaults()
	{
		ProjectileID.Sets.DrawScreenCheckFluff[base.Type] = 10000;
	}

	public override void SetDefaults()
	{
		base.Projectile.width = (base.Projectile.height = 2);
		base.Projectile.tileCollide = false;
		base.Projectile.ignoreWater = true;
		base.Projectile.timeLeft = 900;
	}

	public override void AI()
	{
		//IL_0032: Unknown result type (might be due to invalid IL or missing references)
		//IL_003b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0040: Unknown result type (might be due to invalid IL or missing references)
		//IL_0045: Unknown result type (might be due to invalid IL or missing references)
		//IL_004c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0054: Unknown result type (might be due to invalid IL or missing references)
		//IL_0059: Unknown result type (might be due to invalid IL or missing references)
		//IL_005e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0065: Unknown result type (might be due to invalid IL or missing references)
		//IL_006d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0072: Unknown result type (might be due to invalid IL or missing references)
		//IL_0077: Unknown result type (might be due to invalid IL or missing references)
		//IL_007e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0085: Unknown result type (might be due to invalid IL or missing references)
		//IL_008a: Unknown result type (might be due to invalid IL or missing references)
		//IL_008f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0092: Unknown result type (might be due to invalid IL or missing references)
		//IL_0093: Unknown result type (might be due to invalid IL or missing references)
		//IL_0094: Unknown result type (might be due to invalid IL or missing references)
		//IL_0095: Unknown result type (might be due to invalid IL or missing references)
		//IL_009f: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b6: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b7: Unknown result type (might be due to invalid IL or missing references)
		//IL_00b9: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ba: Unknown result type (might be due to invalid IL or missing references)
		//IL_00c4: Unknown result type (might be due to invalid IL or missing references)
		//IL_00db: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dc: Unknown result type (might be due to invalid IL or missing references)
		//IL_00dd: Unknown result type (might be due to invalid IL or missing references)
		//IL_00de: Unknown result type (might be due to invalid IL or missing references)
		//IL_00e8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ff: Unknown result type (might be due to invalid IL or missing references)
		//IL_0100: Unknown result type (might be due to invalid IL or missing references)
		//IL_0102: Unknown result type (might be due to invalid IL or missing references)
		//IL_0103: Unknown result type (might be due to invalid IL or missing references)
		//IL_010d: Unknown result type (might be due to invalid IL or missing references)
		if (topLine == null)
		{
			float arenaHalfLength = ((base.Projectile.ai[0] == 1f) ? 1000f : 1250f);
			Vector2 topLeft = base.Projectile.Center + new Vector2(0f - arenaHalfLength, 0f - arenaHalfLength);
			Vector2 topRight = base.Projectile.Center + new Vector2(arenaHalfLength, 0f - arenaHalfLength);
			Vector2 bottomLeft = base.Projectile.Center + new Vector2(0f - arenaHalfLength, arenaHalfLength);
			Vector2 bottomRight = base.Projectile.Center + new Vector2(arenaHalfLength, arenaHalfLength);
			topLine = new BloomLineVFX(topLeft, topRight - topLeft, 1f, Color.Red, 900, capped: true, telegraph: true);
			bottomLine = new BloomLineVFX(bottomLeft, bottomRight - bottomLeft, 1f, Color.Red, 900, capped: true, telegraph: true);
			leftLine = new BloomLineVFX(topLeft, bottomLeft - topLeft, 1f, Color.Red, 900, capped: true, telegraph: true);
			rightLine = new BloomLineVFX(topRight, bottomRight - topRight, 1f, Color.Red, 900, capped: true, telegraph: true);
			GeneralParticleHandler.SpawnParticle(topLine);
			GeneralParticleHandler.SpawnParticle(bottomLine);
			GeneralParticleHandler.SpawnParticle(leftLine);
			GeneralParticleHandler.SpawnParticle(rightLine);
		}
		if (NPC.AnyNPCs(ModContent.NPCType<SupremeCalamitas>()))
		{
			base.Projectile.Kill();
		}
	}

	public override void OnKill(int timeLeft)
	{
		topLine.Time = 870;
		bottomLine.Time = 870;
		leftLine.Time = 870;
		rightLine.Time = 870;
	}
}
