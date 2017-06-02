import { FirstTestPage } from './app.po';

describe('first-test App', () => {
  let page: FirstTestPage;

  beforeEach(() => {
    page = new FirstTestPage();
  });

  it('should display welcome message', done => {
    page.navigateTo();
    page.getParagraphText()
      .then(msg => expect(msg).toEqual('Welcome to app!!'))
      .then(done, done.fail);
  });
});
