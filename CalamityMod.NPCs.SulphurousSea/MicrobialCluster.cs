using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace CalamityMod.NPCs.SulphurousSea;

public class MicrobialCluster : ModNPC
{
	public const int ChargeRate = 120;

	public const int SlowdownTime = 45;

	public override void SetStaticDefaults()
	{
		this.HideFromBestiary();
	}

	public override void SetDefaults()
	{
		base.NPC.noGravity = true;
		base.NPC.damage = 0;
		base.NPC.width = 24;
		base.NPC.height = 24;
		base.NPC.lifeMax = 5;
		NPC nPC = base.NPC;
		int aiStyle = (base.AIType = -1);
		nPC.aiStyle = aiStyle;
		base.NPC.noTileCollide = false;
		base.NPC.noGravity = true;
		base.NPC.HitSound = SoundID.NPCHit1;
		base.NPC.DeathSound = SoundID.NPCDeath1;
		base.NPC.dontTakeDamageFromHostiles = true;
		base.NPC.knockBackResist = 0f;
	}

	public override void AI()
	{
		//IL_0021: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0030: Unknown result type (might be due to invalid IL or missing references)
		//IL_0041: Unknown result type (might be due to invalid IL or missing references)
		//IL_0046: Unknown result type (might be due to invalid IL or missing references)
		//IL_0049: Unknown result type (might be due to invalid IL or missing references)
		//IL_0053: Unknown result type (might be due to invalid IL or missing references)
		//IL_0058: Unknown result type (might be due to invalid IL or missing references)
		//IL_0063: Unknown result type (might be due to invalid IL or missing references)
		//IL_006e: Unknown result type (might be due to invalid IL or missing references)
		//IL_0079: Unknown result type (might be due to invalid IL or missing references)
		//IL_0083: Unknown result type (might be due to invalid IL or missing references)
		//IL_0088: Unknown result type (might be due to invalid IL or missing references)
		//IL_00ee: Unknown result type (might be due to invalid IL or missing references)
		//IL_00f8: Unknown result type (might be due to invalid IL or missing references)
		//IL_00fd: Unknown result type (might be due to invalid IL or missing references)
		//IL_0128: Unknown result type (might be due to invalid IL or missing references)
		//IL_012d: Unknown result type (might be due to invalid IL or missing references)
		//IL_0132: Unknown result type (might be due to invalid IL or missing references)
		//IL_0137: Unknown result type (might be due to invalid IL or missing references)
		//IL_0145: Unknown result type (might be due to invalid IL or missing references)
		//IL_014f: Unknown result type (might be due to invalid IL or missing references)
		//IL_0154: Unknown result type (might be due to invalid IL or missing references)
		//IL_0179: Unknown result type (might be due to invalid IL or missing references)
		//IL_018c: Unknown result type (might be due to invalid IL or missing references)
		//IL_0192: Unknown result type (might be due to invalid IL or missing references)
		//IL_019e: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ac: Unknown result type (might be due to invalid IL or missing references)
		//IL_01c5: Unknown result type (might be due to invalid IL or missing references)
		//IL_01ca: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.collideX || base.NPC.collideY)
		{
			NPC nPC = base.NPC;
			nPC.velocity *= -1f;
			base.NPC.netUpdate = true;
		}
		Color newColor = Color.GreenYellow;
		DelegateMethods.v3_1 = ((Color)(ref newColor)).ToVector3() * 2f;
		Utils.PlotTileLine(base.NPC.Center, base.NPC.Center + base.NPC.velocity * 10f, 8f, DelegateMethods.CastLightOpen);
		base.NPC.ai[0]++;
		if (base.NPC.ai[0] % 45f > 0f)
		{
			NPC nPC2 = base.NPC;
			nPC2.velocity *= 0.98f;
		}
		if (base.NPC.ai[0] % 45f == 44f)
		{
			base.NPC.velocity = base.NPC.velocity.SafeNormalize(-Vector2.UnitY).RotatedByRandom(0.7853981852531433) * 4f;
		}
		if (base.NPC.ai[0] % 32f == 31f)
		{
			Vector2 center = base.NPC.Center;
			newColor = default(Color);
			Dust dust = Dust.NewDustPerfect(center, 75, null, 0, newColor);
			dust.velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(1f, 2f);
			dust.noGravity = true;
			dust.scale = 1.6f;
		}
	}

	public override float SpawnChance(NPCSpawnInfo spawnInfo)
	{
		if (!spawnInfo.Player.InSulphur() || !spawnInfo.Water)
		{
			return 0f;
		}
		return 0.4f;
	}

	public override void HitEffect(NPC.HitInfo hit)
	{
		//IL_0018: Unknown result type (might be due to invalid IL or missing references)
		//IL_002b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0031: Unknown result type (might be due to invalid IL or missing references)
		//IL_003d: Unknown result type (might be due to invalid IL or missing references)
		//IL_004b: Unknown result type (might be due to invalid IL or missing references)
		//IL_0064: Unknown result type (might be due to invalid IL or missing references)
		//IL_0069: Unknown result type (might be due to invalid IL or missing references)
		if (base.NPC.life <= 0)
		{
			for (int k = 0; k < 6; k++)
			{
				Dust dust = Dust.NewDustPerfect(base.NPC.Center, 75);
				dust.velocity = Vector2.One.RotatedByRandom(6.2831854820251465) * Main.rand.NextFloat(1f, 2f);
				dust.scale = 1.2f;
			}
		}
	}
}
