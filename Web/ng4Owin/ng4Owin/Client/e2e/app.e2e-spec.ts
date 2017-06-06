import { Ng4OwinPage } from './app.po';

describe('ng4-owin App', () => {
  let page: Ng4OwinPage;

  beforeEach(() => {
    page = new Ng4OwinPage();
  });

  it('should display welcome message', done => {
    page.navigateTo();
    page.getParagraphText()
      .then(msg => expect(msg).toEqual('Welcome to app!!'))
      .then(done, done.fail);
  });
});
