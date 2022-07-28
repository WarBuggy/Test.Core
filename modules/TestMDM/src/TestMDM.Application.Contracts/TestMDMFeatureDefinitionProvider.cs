using Volo.Abp.Features;
using Volo.Abp.Localization;
using Volo.Abp.Validation.StringValues;
using TestMDM.Localization;

namespace TestMDM
{
    public class TestMDMFeatureDefinitionProvider : FeatureDefinitionProvider
    {
        public override void Define(IFeatureDefinitionContext context)
        {
            var masterGroup = context.AddGroup(TestMDMFeatures.GroupName);

            var enable = masterGroup.AddFeature(
                TestMDMFeatures.Enable,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<TestMDMResource>("EnableMDM"),
                valueType: new ToggleStringValueType()
            );

            enable.CreateChild(
               TestMDMFeatures.Distributor,
                defaultValue: "false",
                displayName: LocalizableString
                                 .Create<TestMDMResource>("EnableDistributor"),
                valueType: new ToggleStringValueType()
            );
        }
    }
}