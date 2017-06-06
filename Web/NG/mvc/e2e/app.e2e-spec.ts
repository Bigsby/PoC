import { MvcPage } from './app.po';

describe('mvc App', () => {
  let page: MvcPage;

  beforeEach(() => {
    page = new MvcPage();
  });

  it('should display welcome message', done => {
    page.navigateTo();
    page.getParagraphText()
      .then(msg => expect(msg).toEqual('Welcome to app!!'))
      .then(done, done.fail);
  });
});
