namespace ExternalSearch.FullContact.Integration.Tests
{
    public class FullContactTests
    {
        // TODO Issue 170 - Test Failures
        //[Fact]
        //public void Test()
        //{
        //    // Arrange
        //    this.testContext = new TestContext();
        //    var properties = new EntityMetadataPart();
        //    properties.Properties.Add(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInOrganization.Website, "http://cluedin.com");

        //    IEntityMetadata entityMetadata = new EntityMetadataPart()
        //        {
        //            Name        = "CluedIn",
        //            EntityType  = EntityType.Organization
        //        };

        //    var externalSearchProvider  = new Mock<FullContactCompanyDomainExternalSearchProvider>(MockBehavior.Loose);
        //    var clues                   = new List<CompressedClue>();

        //    externalSearchProvider.CallBase = true;

        //    this.testContext.ProcessingHub.Setup(h => h.SendCommand(It.IsAny<ProcessClueCommand>())).Callback<IProcessingCommand>(c => clues.Add(((ProcessClueCommand)c).Clue));

        //    this.testContext.Container.Register(Component.For<IExternalSearchProvider>().UsingFactoryMethod(() => externalSearchProvider.Object));

        //    var context         = this.testContext.Context.ToProcessingContext();
        //    var command         = new ExternalSearchCommand();
        //    var actor           = new ExternalSearchProcessing(context.ApplicationContext);
        //    var workflow        = new Mock<Workflow>(MockBehavior.Loose, context, new EmptyWorkflowTemplate<ExternalSearchCommand>());
            
        //    workflow.CallBase = true;

        //    command.With(context);
        //    command.OrganizationId  = context.Organization.Id;
        //    command.EntityMetaData  = entityMetadata;
        //    command.Workflow        = workflow.Object;
        //    context.Workflow        = command.Workflow;

        //    // Act
        //    var result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Repeat.SaveResult, result.SaveResult);

        //    result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Success.SaveResult, result.SaveResult);
        //    context.Workflow.AddStepResult(result);
            
        //    context.Workflow.ProcessStepResult(context, command);

        //    // Assert
        //    this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.AtLeastOnce);

        //    Assert.True(clues.Count > 0);
        //}

        //[Fact]
        //public void TestWebsiteLookup()
        //{
        //    // Arrange
        //    this.testContext = new TestContext();
        //    var properties = new EntityMetadataPart();
        //    properties.Properties.Add(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.Email, "tiw@cluedin.com");
        
        //    IEntityMetadata entityMetadata = new EntityMetadataPart()
        //    {
        //        Name = "Tim Ward",
        //        EntityType = EntityType.Person,
        //        Properties = properties.Properties
        //    };

        //    var externalSearchProvider = new Mock<FullContactEmailExternalSearchProvider>(MockBehavior.Loose);
        //    var clues = new List<CompressedClue>();

        //    externalSearchProvider.CallBase = true;

        //    this.testContext.ProcessingHub.Setup(h => h.SendCommand(It.IsAny<ProcessClueCommand>())).Callback<IProcessingCommand>(c => clues.Add(((ProcessClueCommand)c).Clue));

        //    this.testContext.Container.Register(Component.For<IExternalSearchProvider>().UsingFactoryMethod(() => externalSearchProvider.Object));

        //    var context = this.testContext.Context.ToProcessingContext();
        //    var command = new ExternalSearchCommand();
        //    var actor = new ExternalSearchProcessing(context.ApplicationContext);
        //    var workflow = new Mock<Workflow>(MockBehavior.Loose, context, new EmptyWorkflowTemplate<ExternalSearchCommand>());

        //    workflow.CallBase = true;

        //    command.With(context);
        //    command.OrganizationId = context.Organization.Id;
        //    command.EntityMetaData = entityMetadata;
        //    command.Workflow = workflow.Object;
        //    context.Workflow = command.Workflow;

        //    // Act
        //    var result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Repeat.SaveResult, result.SaveResult);

        //    result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Success.SaveResult, result.SaveResult);
        //    context.Workflow.AddStepResult(result);

        //    context.Workflow.ProcessStepResult(context, command);

        //    // Assert
        //    this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.AtLeastOnce);

        //    Assert.True(clues.Count > 0);
        //}

        //[Fact]
        //public void TestRelations()
        //{
        //    // Arrange
        //    this.testContext = new TestContext();
        //    var properties = new EntityMetadataPart();
        //    properties.Properties.Add(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInPerson.PhoneNumber, "53841516");

        //    IEntityMetadata entityMetadata = new EntityMetadataPart()
        //    {
        //        Name = "Tim Ward",
        //        EntityType = EntityType.Person,
        //        Properties = properties.Properties
        //    };

        //    var externalSearchProvider = new Mock<FullContactPhoneExternalSearchProvider>(MockBehavior.Loose);

        //    var clues = new List<CompressedClue>();

        //    externalSearchProvider.CallBase = true;

        //    this.testContext.ProcessingHub.Setup(h => h.SendCommand(It.IsAny<ProcessClueCommand>())).Callback<IProcessingCommand>(c => clues.Add(((ProcessClueCommand)c).Clue));

        //    this.testContext.Container.Register(Component.For<IExternalSearchProvider>().UsingFactoryMethod(() => externalSearchProvider.Object));

        //    var context = this.testContext.Context.ToProcessingContext();
        //    var command = new ExternalSearchCommand();
        //    var actor = new ExternalSearchProcessing(context.ApplicationContext);
        //    var workflow = new Mock<Workflow>(MockBehavior.Loose, context, new EmptyWorkflowTemplate<ExternalSearchCommand>());

        //    workflow.CallBase = true;

        //    command.With(context);
        //    command.OrganizationId = context.Organization.Id;
        //    command.EntityMetaData = entityMetadata;
        //    command.Workflow = workflow.Object;
        //    context.Workflow = command.Workflow;

        //    // Act
        //    var result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Repeat.SaveResult, result.SaveResult);

        //    result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Success.SaveResult, result.SaveResult);
        //    context.Workflow.AddStepResult(result);

        //    context.Workflow.ProcessStepResult(context, command);

        //    // Assert
        //    this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.AtLeastOnce);

        //    Assert.True(clues.Count > 0);
        //}

        //[Fact]
        //public void TestPhoto()
        //{
        //    // Arrange
        //    this.testContext = new TestContext();
        //    var properties = new EntityMetadataPart();
        //    properties.Properties.Add(CluedIn.Core.Data.Vocabularies.Vocabularies.CluedInUser.SocialTwitter, "https://twitter.com/jerrong");

        //    IEntityMetadata entityMetadata = new EntityMetadataPart()
        //    {
        //        Name = "Tim Ward",
        //        EntityType = EntityType.Person,
        //        Properties = properties.Properties
        //    };

        //    var externalSearchProvider = new Mock<FullContactTwitterExternalSearchProvider>(MockBehavior.Loose);
        //    var clues = new List<CompressedClue>();

        //    externalSearchProvider.CallBase = true;

        //    this.testContext.ProcessingHub.Setup(h => h.SendCommand(It.IsAny<ProcessClueCommand>())).Callback<IProcessingCommand>(c => clues.Add(((ProcessClueCommand)c).Clue));

        //    this.testContext.Container.Register(Component.For<IExternalSearchProvider>().UsingFactoryMethod(() => externalSearchProvider.Object));

        //    var context = this.testContext.Context.ToProcessingContext();
        //    var command = new ExternalSearchCommand();
        //    var actor = new ExternalSearchProcessing(context.ApplicationContext);
        //    var workflow = new Mock<Workflow>(MockBehavior.Loose, context, new EmptyWorkflowTemplate<ExternalSearchCommand>());

        //    workflow.CallBase = true;

        //    command.With(context);
        //    command.OrganizationId = context.Organization.Id;
        //    command.EntityMetaData = entityMetadata;
        //    command.Workflow = workflow.Object;
        //    context.Workflow = command.Workflow;

        //    // Act
        //    var result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Repeat.SaveResult, result.SaveResult);

        //    result = actor.ProcessWorkflowStep(context, command);
        //    Assert.Equal(WorkflowStepResult.Success.SaveResult, result.SaveResult);
        //    context.Workflow.AddStepResult(result);

        //    context.Workflow.ProcessStepResult(context, command);

        //    // Assert
        //    this.testContext.ProcessingHub.Verify(h => h.SendCommand(It.IsAny<ProcessClueCommand>()), Times.AtLeastOnce);

        //    Assert.True(clues.Count > 0);
        //}
    }
}
