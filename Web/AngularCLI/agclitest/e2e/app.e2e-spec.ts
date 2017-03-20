import { AgclitestPage } from './app.po';

describe('agclitest App', () => {
  let page: AgclitestPage;

  beforeEach(() => {
    page = new AgclitestPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
