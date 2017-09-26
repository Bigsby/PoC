import { PackageSearchPage } from './app.po';

describe('package-search App', () => {
  let page: PackageSearchPage;

  beforeEach(() => {
    page = new PackageSearchPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!');
  });
});
